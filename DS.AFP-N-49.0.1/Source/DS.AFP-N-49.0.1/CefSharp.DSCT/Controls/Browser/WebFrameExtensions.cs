using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefSharp.DSCT.Controls
{
    public static class  WebFrameExtensions
    {

        private static Type[] numberTypes = new Type[] { typeof(int), typeof(uint), typeof(double), typeof(decimal), typeof(float), typeof(Int64), typeof(Int16) };

        /// <summary>
        /// Execute some Javascript code in the context of this WebBrowser. As the method name implies, the script will be
        /// executed asynchronously, and the method therefore returns before the script has actually been executed.
        /// This simple helper extension will encapsulate params in single quotes (unless int, uint, etc)
        /// </summary>
        /// <param name="browser">The ChromiumWebBrowser instance this method extends</param>
        /// <param name="methodName">The javascript method name to execute</param>
        /// <param name="args">the arguments to be passed as params to the method</param>
        public static void ExecuteScriptAsync(this IFrame frame, string methodName, params object[] args)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(methodName);
            stringBuilder.Append("(");

            if(args.Length > 0)
            { 
                for (int i = 0; i < args.Length; i++)
                {
                    var obj = args[i];
                    if(obj == null)
                    {
                        stringBuilder.Append("null");
                    }
                    else
                    {
                        var encapsulateInSingleQuotes = !numberTypes.Contains(obj.GetType());
                        if(encapsulateInSingleQuotes)
                        {
                            stringBuilder.Append("'");
                        }

                        stringBuilder.Append(args[i].ToString());

                        if (encapsulateInSingleQuotes)
                        {
                            stringBuilder.Append("'");
                        }
                    }

                    stringBuilder.Append(", ");
                }
            
                //Remove the trailing comma
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }

            stringBuilder.Append(");");

            var script = stringBuilder.ToString();

            frame.ExecuteJavaScriptAsync(script);
        }

    
    }
}
