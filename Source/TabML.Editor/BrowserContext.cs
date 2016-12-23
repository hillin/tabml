using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CefSharp.Wpf;

namespace TabML.Editor
{
    static class BrowserContext
    {
        public class AsyncJavascriptCallback
        {
            public Dictionary<string, object> Result { get; private set; }
            public bool IsCalledBack { get; private set; }
            
            private readonly SemaphoreSlim _semaphore;

            public AsyncJavascriptCallback()
            {
                _semaphore = new SemaphoreSlim(1);
            }

            public void Callback(Dictionary<string, object> result)
            {
                this.Result = result;
                this.IsCalledBack = true;
            }

            public async Task Acquire()
            {
                await _semaphore.WaitAsync();
                this.IsCalledBack = false;
                this.Result = null;
            }

            public void Release()
            {
                _semaphore.Release();
            }
        }

        public static AsyncJavascriptCallback CallbackObject { get; private set; }

        public static void RegisterCallbackObject(ChromiumWebBrowser browser)
        {
            BrowserContext.CallbackObject = new AsyncJavascriptCallback();
            browser.RegisterJsObject("__callbackObject", BrowserContext.CallbackObject);
        }

    }
}
