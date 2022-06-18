using System;

namespace Domain
{
    public abstract class AbstractContent
    {

        #region Shared Methods

        protected string GetQuote(string message)
        {
            // TODO: implement message/quote pairs
            return null;
            /*if (!message.Contains("&gt;"))
            {
                return null;
            }

            string quote;
            try
            {
                int startQuoteIndex = message.IndexOf("&gt;") + "&gt;".Length;
                int stopQuoteIndex = message.IndexOf("\n\n");
                quote = message.Substring(startQuoteIndex, stopQuoteIndex - startQuoteIndex);
            }
            catch (Exception)
            {
                quote = null;
            }

            return quote;*/
        }

        #endregion

    }
}
