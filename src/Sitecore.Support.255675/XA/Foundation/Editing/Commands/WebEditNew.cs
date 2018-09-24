namespace Sitecore.Support.XA.Foundation.Editing.Commands
{
    using Sitecore.Data.Items;
    using Sitecore.Globalization;
    using Sitecore.Links;
    using Sitecore.Sites;
    using Sitecore.Web;
    using Sitecore.Web.UI.Sheer;
    using System.Collections.Specialized;
    public class WebEditNew : Sitecore.XA.Foundation.Editing.Commands.WebEditNew
    {
        protected override void ReloadOrNavigate(Item newItem, NameValueCollection parameters)
        {
            if (newItem.Visualization.Layout == null || !MainUtil.GetBool(parameters["navigate"], true))
            {
                #region sitecore.support.255675  
                //SheerResponse.Eval("window.top.location.reload(true)");
                SheerResponse.Eval("if (window.top.location.href.includes(\"sc_mode=edit\") == true) {window.top.location.reload();} else {window.parent.location.reload(true)}");
                #endregion sitecore.support.255675  
                return;
            }
            UrlOptions defaultOptions = UrlOptions.DefaultOptions;
            SiteContext site = SiteContext.GetSite(string.IsNullOrEmpty(parameters["sc_pagesite"]) ? WebEditUtil.SiteName : parameters["sc_pagesite"]);
            if (site == null)
            {
                return;
            }
            using (new SiteContextSwitcher(site))
            {
                using (new LanguageSwitcher(newItem.Language))
                {
                    string itemUrl = LinkManager.GetItemUrl(newItem, defaultOptions);
                    SheerResponse.Eval("scNavigate(\"" + itemUrl + "\", 1)");
                }
            }
        }

    }
}

