using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Utils.MaterialIO
{
    [ToolboxData("<{0}:MaterialSidebar runat=server></{0}:MaterialSidebar>")]
    public class MaterialSidebar : WebControl, IPostBackEventHandler
    {
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<SidebarGroup> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
            }
        }
        public List<SidebarGroup> _groups
        {
            get { return ViewState["Groups"] as List<SidebarGroup>; }
            set
            {
                ViewState["Groups"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            var stringBuilder = new StringBuilder();
            string selectedPage = HttpContext.Current.Request.Url.AbsolutePath;
            for (int i = 0; i < _groups.Count; i++)
            {
                var group = _groups[i];
                foreach(var item in group.SidebarItems)
                {
                    if (item.RedirectionPage.Equals(selectedPage) ||
                        (item.AlsoSelectedForPages != null && item.AlsoSelectedForPages.Any(page => page.Equals(selectedPage))))
                    {
                        item.CurrentlySelected = true;
                    }
                    else
                    {
                        item.CurrentlySelected = false;
                    }
                }
                group.Output(stringBuilder);

                if(i != (_groups.Count - 1))
                {
                    stringBuilder.Append(@"<hr class=""mdc-list-divider"">");
                }

            }

            output.Write(stringBuilder.ToString());

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "materialio_selection_nav_postback",
                this.ResolveUrl("~/Scripts/ProjectFlow/materialio_selection_nav_postback.js"));
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, base.UniqueID);
        }

        public delegate void NavClickListener(string rediectionPage);
        public event NavClickListener NavClickListeners;
        public void RaisePostBackEvent(string eventArgument)
        {
            NavClickListeners(eventArgument);
        }

    }

    [Serializable]
    public class SidebarItem : DOMElement, Renderer
    {
        private new CustomAttribute CustomAttribute;
        private DOMElement spanLabel;
        private DOMElement graphic;
        public string RedirectionPage { get; set; }
        public string FASLogo { get; set; }

        [TypeConverter(typeof(StringArrayConverter))]
        public string[] AlsoSelectedForPages { get; set; }
        public string ItemText { get => spanLabel.Text; set
            {
                spanLabel.Text = value;

            }
        }
        public bool CurrentlySelected { get; set; }

        public SidebarItem(): this("", "")
        {

        }

        public SidebarItem(string label, string redirectionPage) : base("a")
        {
            AddAttribute(new CustomAttribute("href", "#"));
            RedirectionPage = redirectionPage;
            AddClass("mdc-list-item");
            spanLabel = new DefaultDOMElement("span");
            spanLabel.AddClass("mdc-list-item__text");
            spanLabel.Text = label;

            graphic = new DefaultDOMElement("i");
            graphic.AddClass("material-icons");
            graphic.AddClass("mdc-list-item__graphic");
            graphic.AddClass("fas");
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            if (!String.IsNullOrEmpty(FASLogo))
            {
                graphic.RemoveClass("fa-" + FASLogo);
                graphic.AddClass("fa-" + FASLogo);
                stringBuilder.Append(graphic.GetHTML());
            }

            stringBuilder.Append(spanLabel.GetHTML());
        }

        public void Output(StringBuilder stringBuilder)
        {
            if (CurrentlySelected)
            {
                AddClass("mdc-list-item--activated");
            }
            else
            {
                RemoveClass("mdc-list-item--activated");
            }

            //Add redirection
            if (!String.IsNullOrEmpty(RedirectionPage))
            {
                CustomAttribute = new CustomAttribute("data-href", RedirectionPage);
                AddAttribute(CustomAttribute);
            }
            else
            {
                if(CustomAttribute!=null) RemoveAttribute(CustomAttribute);
            }

            stringBuilder.Append(GetHTML());
        }
    }

    [Serializable]
    public class SidebarSubHeader : DOMElement, Renderer
    {
        public string Header { get; set; }

        public SidebarSubHeader(string header) : base("h6")
        {
            this.Text = header;
            AddClass("mdc-list-group__subheader");
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            
        }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(GetHTML());
        }
    }

    [Serializable]
    [ParseChildren(true)]
    public class SidebarGroup : Renderer
    {
        public SidebarSubHeader SubHeader;


        public string SubHeaderText { get => SubHeader.Text; set
            {
                SubHeader = new SidebarSubHeader(value);
            }
        }

        public SidebarGroup(): this("")
        {
        }

        public SidebarGroup(string subheader)
        {
            SubHeader = new SidebarSubHeader(subheader);
        }

        private readonly List<SidebarItem> _sidebarItems = new List<SidebarItem>();

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<SidebarItem> SidebarItems { get => _sidebarItems; }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(SubHeader.GetHTML());

            foreach (var item in _sidebarItems)
            {
                item.Output(stringBuilder);
            }

        }
    }

}
