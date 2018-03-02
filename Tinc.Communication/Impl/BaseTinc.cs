using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tinc.BL;
using Tinc.Communication.Properties;

namespace Tinc.Communication.Impl
{
    public abstract class BaseTinc : IDisposable
    {
        protected TincProperties mProperties;
        protected Process mTincProcess;
        protected Process mTincDaemonProcess;

        protected BaseTinc(TincProperties properties)
        {
            mProperties = properties;
            InitializeProcesses();
        }

        protected void StartDaemon()
        {
            mTincDaemonProcess.StartInfo.Arguments = $"-n {mProperties.TincNetName} -D -d3";
            mTincDaemonProcess.Start();
            mTincDaemonProcess.BeginOutputReadLine();
            mTincDaemonProcess.BeginErrorReadLine();
        }

        protected virtual void Configure()
        {
            CreateNode(mProperties.TincNetName, mProperties.TincNodeName);
            SetSubnet(mProperties.TincNetName);
            AdvancedConfigureTinc();
            ConfigureNetworkInterface();
        }

        protected void Terminate()
        {
            TerminateTinc();
            TerminateTincDaemon();
        }

        protected abstract void AdvancedConfigureTinc();

        private void InitializeProcesses()
        {
            mTincProcess = new Process
            {
                StartInfo = new ProcessStartInfo(Settings.Default.TincExec)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };
            mTincDaemonProcess = new Process
            {
                StartInfo = new ProcessStartInfo(Settings.Default.TincDaemon)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                }
            };

            mTincDaemonProcess.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            mTincDaemonProcess.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);
            mTincProcess.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            mTincProcess.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);
        }

        private void CreateNode(string netName, string nodeName)
        {
            mTincProcess.StartInfo.Arguments = $"-n {netName} init {nodeName}";
            mTincProcess.Start();
            mTincProcess.BeginOutputReadLine();
            mTincProcess.BeginErrorReadLine();
            mTincProcess.WaitForExit();
        }

        private void SetSubnet(string netName)
        {
            mTincProcess.StartInfo.Arguments = $"-n {netName} add subnet {mProperties.Subnet}";
            mTincProcess.Start();
            mTincProcess.WaitForExit();
        }

        private void ConfigureNetworkInterface()
        {
            var netshStartInfo =
                new ProcessStartInfo
                {
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments =
                        $"/C netsh interface ip set address \"{mProperties.NetworkInterfaceName}\" static {mProperties.Subnet}" +
                        $" 255.255.255.0 {mProperties.Gateway}"
                };

            Process.Start(netshStartInfo)?.WaitForExit();
        }

        private void TerminateTincDaemon()
        {
            if (!mTincDaemonProcess.HasExited)
                mTincDaemonProcess.Kill();
        }

        private void TerminateTinc()
        {
            if (!mTincProcess.HasExited)
                mTincProcess.Kill();
        }

        public void Dispose()
        {
            TerminateTincDaemon();
            TerminateTinc();

            mTincDaemonProcess.Dispose();
            mTincProcess.Dispose();
        }
    }
}