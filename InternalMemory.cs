using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Gx_cheat
{
    internal static class InternalMemory
    {
        [DllImport("AotBst.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern nint CPU(nint pVM, uint cpuId);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação

        [DllImport("AotBst.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern int InternalRead(nint pVM, ulong address, nint buffer, uint size);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação

        [DllImport("AotBst.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern int Cast(nint pVCpu, ulong address, out ulong physAddress);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação

        [DllImport("AotBst.dll", CallingConvention = CallingConvention.Cdecl)]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação
        private static extern int InternalWrite(nint pVM, ulong address, nint buffer, uint size);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' em vez de 'DllImportAttribute' para gerar código de marshalling P/Invoke no tempo de compilação

        private static nint pVMAddr = nint.Zero;
        private static nint cpuAddr = nint.Zero;
        private static Dictionary<ulong, ulong>? Cache;

        internal static bool Initialized =>
            pVMAddr != nint.Zero && Cache != null;

        internal static bool Initialize(nint pVM)
        {
            if (pVM == nint.Zero)
                return false;

            try
            {
                pVMAddr = pVM;
                cpuAddr = CPU(pVMAddr, 0);

                if (cpuAddr == nint.Zero)
                    return false;

                Cache = new Dictionary<ulong, ulong>();
                return true;
            }
            catch
            {
                pVMAddr = nint.Zero;
                cpuAddr = nint.Zero;
                Cache = null;
                return false;
            }
        }

        internal static bool Convert(ulong address, out ulong phys)
        {
            phys = 0;

            if (!Initialized || address == 0)
                return false;

            try
            {
                if (!Config.NoCache && Cache!.TryGetValue(address, out var cached))
                {
                    phys = cached;
                    return true;
                }

                cpuAddr = CPU(pVMAddr, 0);
                if (cpuAddr == nint.Zero)
                    return false;

                var status = Cast(cpuAddr, address, out phys);

                if (status == 0 && phys != 0 && !Config.NoCache)
                    Cache![address] = phys;

                return status == 0 && phys != 0;
            }
            catch
            {
                return false;
            }
        }

        internal static unsafe bool Read<T>(ulong address, out T data) where T : struct
        {
            data = default;

            if (!Convert(address, out var phys))
                return false;

            try
            {
                var size = (uint)Marshal.SizeOf<T>();
                T buffer = default;
                var tr = __makeref(buffer);

#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
                var status = InternalRead(
                    pVMAddr,
                    phys,
                    *(nint*)&tr,
                    size
                );
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado

                if (status != 0)
                    return false;

                data = buffer;
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static unsafe bool ReadArray<T>(ulong address, ref T[] array) where T : struct
        {
            if (array == null || array.Length == 0)
                return false;

            if (!Convert(address, out var phys))
                return false;

            try
            {
                var size = (uint)(Marshal.SizeOf<T>() * array.Length);
                var tr = __makeref(array[0]);

#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
                var status = InternalRead(
                    pVMAddr,
                    phys,
                    *(nint*)&tr,
                    size
                );
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado

                return status == 0;
            }
            catch
            {
                return false;
            }
        }

        internal static string ReadString(ulong address, int size, bool unicode = true)
        {
            if (size <= 0)
                return string.Empty;

            var bytes = new byte[size];

            if (!ReadArray(address, ref bytes))
                return string.Empty;

            try
            {
                var str = unicode
                    ? Encoding.Unicode.GetString(bytes)
                    : Encoding.Default.GetString(bytes);

                var idx = str.IndexOf('\0');
                return idx >= 0 ? str[..idx] : str;
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static unsafe bool Write<T>(ulong address, T value) where T : struct
        {
            if (!Convert(address, out var phys))
                return false;

            try
            {
                var size = (uint)Marshal.SizeOf<T>();
                var tr = __makeref(value);

#pragma warning disable CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado
                var status = InternalWrite(
                    pVMAddr,
                    phys,
                    *(nint*)&tr,
                    size
                );
#pragma warning restore CS8500 // Isso pega o endereço, obtém o tamanho ou declara um ponteiro para um tipo gerenciado

                return status == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
