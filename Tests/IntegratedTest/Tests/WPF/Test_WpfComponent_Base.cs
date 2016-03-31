﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HTML_WPF.Component;
using IntegratedTest.Infra.Window;

namespace IntegratedTest.Tests.WPF
{
    public abstract class Test_WpfComponent_Base<T>  where T : HTMLControlBase
    {
        private readonly IWindowTestEnvironment _WindowTestEnvironment;
        protected Test_WpfComponent_Base(IWindowTestEnvironment windowTestEnvironment) 
        {
            _WindowTestEnvironment = windowTestEnvironment;
        }

        private WindowTest BuildWindow(Func<T> iWebControlFac, bool iManageLifeCycle)
        {
            return new WindowTest(_WindowTestEnvironment,
                (w) =>
                {
                    var stackPanel = new StackPanel() 
                    {
                        Height = 400
                    };
                    w.Content = stackPanel;
                    var iWebControl = iWebControlFac();
                    iWebControl.Height = 400;
                    if (iManageLifeCycle)
                        w.Closed += (o, e) => { iWebControl.Dispose(); };
                    stackPanel.Children.Add(iWebControl);
                });
        }

        protected abstract T GetNewHTMLControlBase(bool iDebug);

        private class HTMLControlBase_Handler<Thandler> 
        {
            public Thandler Handler { get; set; }
        }

        private WindowTest InitTest(HTMLControlBase_Handler<T> handler, bool iDebug = false, bool iManageLifeCycle = true) 
        {
            AssemblyHelper.SetEntryAssembly();
            Func<T> iWebControlFac = () => 
            {
                return handler.Handler = GetNewHTMLControlBase(iDebug);
            };

            return BuildWindow(iWebControlFac, iManageLifeCycle);
        }

        internal void Test(Action<T> test, bool iDebug = false, bool iManageLifeCycle = true) 
        {
            var handler = new HTMLControlBase_Handler<T>();
            using (var wcontext = InitTest(handler, iDebug, iManageLifeCycle))
            {
                wcontext.RunOnUIThread( () => test(handler.Handler)).Wait();
            }
        }

        internal async Task Test(Func<T, WindowTest, Task> test, bool iDebug = false, bool iManageLifeCycle = true) 
        {
            var handler = new HTMLControlBase_Handler<T>();
            using (var wcontext = InitTest(handler, iDebug, iManageLifeCycle)) 
            {
                await wcontext.RunOnUIThread(async () => await test(handler.Handler, wcontext));
            }
        }
    }
}