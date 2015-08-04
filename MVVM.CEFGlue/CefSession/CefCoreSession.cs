﻿using CefGlue.Window;
using MVVM.CEFGlue.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xilium.CefGlue;

namespace MVVM.CEFGlue.CefSession
{
    public class CefCoreSession : ICefCoreSession, IDisposable
    {
        private CefSettings _CefSettings;
        private string[] _Args;
        private MVVMCefApp _CefApp;
        private IUIDispatcher _Dispatcher;

        public CefCoreSession(IUIDispatcher iIUIDispatcher, CefSettings iCefSettings, MVVMCefApp iCefApp, string[] iArgs)
        {
            _Dispatcher = iIUIDispatcher;
            _CefApp = iCefApp;
            _Args = iArgs;
            _CefSettings = iCefSettings;
            var mainArgs = new CefMainArgs(_Args);

            CefRuntime.Load();

            var exitCode = CefRuntime.ExecuteProcess(mainArgs, _CefApp, IntPtr.Zero);
            if (exitCode != -1)
                throw ExceptionHelper.Get(string.Format("Unable to execute cef process: {0}", exitCode));

            CefRuntime.Initialize(mainArgs, _CefSettings, _CefApp, IntPtr.Zero);
        }

        public IUIDispatcher Dispatcher
        {
            get { return _Dispatcher; }
        }

        public MVVMCefApp CefApp 
        { 
            get { return _CefApp; } 
        }

        public CefSettings CefSettings
        {
            get { return _CefSettings; }
        }


        public void Dispose()
        {
            CefRuntime.Shutdown();
        }
    }
}
