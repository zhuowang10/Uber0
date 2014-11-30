using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CacheAccessWebSite
{
    public partial class _Default : System.Web.UI.Page
    {
        public string NameText { get; set; }
        public string ResultText { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void BtnCall_Click(object sender, EventArgs e)
        {
            long startTick, tick;            
            CacheServiceClient.CacheServiceClient proxy = new CacheServiceClient.CacheServiceClient();
            NameText = txtName.Text;
            startTick = DateTime.UtcNow.Ticks;            
            string rs = proxy.GetHelloWorldWithCache(NameText);
            tick = DateTime.UtcNow.Ticks;
            ResultText = string.Format("call return: {0}, latency: {1}", rs, (tick-startTick).ToString());
            lbResult.Text = ResultText;
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            long startTick, tick;
            CacheServiceClient.CacheServiceClient proxy = new CacheServiceClient.CacheServiceClient();
            NameText = txtName.Text;
            startTick = DateTime.UtcNow.Ticks;
            proxy.ClearHelloWorldCache(NameText);
            tick = DateTime.UtcNow.Ticks;
            ResultText = string.Format("cache cleared, latency: {0}", (tick - startTick).ToString());
            lbResult.Text = ResultText;
        }
    }
}
