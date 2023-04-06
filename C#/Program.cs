using System;
using System.Threading;
using System.Threading.Tasks;

#pragma warning disable IDE0090

namespace 
{
    public static class Program
    {
        private static readonly CancellationTokenSource cts = new CancellationTokenSource();

        private static void CancelHandler(object? sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = false;

            cts.Cancel();
        }

        public static async Task<int> Main(string[] args)
        {
            Console.CancelKeyPress += CancelHandler;

            int exitCode = 0;

            try
            {
                exitCode = await InnerProgram.InnerMain(args, cts.Token).ConfigureAwait(false);
            }
            catch (OperationCanceledException)
            {
                await Console.Out.WriteLineAsync("cancelled").ConfigureAwait(false);

                exitCode = -1;
            }
            finally
            {
                cts.Dispose();

                Console.CancelKeyPress -= CancelHandler;
            }

            return exitCode;
        }
    }
}

#pragma warning restore IDE0090