using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectFlow.Utils.MaterialIO
{
    [ToolboxData("<{0}:MaterialIOTableControl runat=server></{0}:MaterialIOTableControl>")]
    public class MaterialIOTableControl: WebControl
    {
        public MaterialIOTable Table = new MaterialIOTable();

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.Write(Table.GetHTML());
        }

        public class MaterialIOTable : DOMElement, Renderer
        {
            private DefaultDOMElement Table;
            public MaterialIOTableHead Head = new MaterialIOTableHead();

            public MaterialIOTable() : base("div")
            {
                AddClass("mdc-data-table");
                Table = new DefaultDOMElement("table");
                Table.AddClass("mdc-data-table__table");
                AddAttribute(new CustomAttribute("aria-label", "Dessert calories"));
            }

            public override void InsertElement(StringBuilder stringBuilder)
            {
                stringBuilder.Append(Head.GetHTML());
            }

            public void Output(StringBuilder stringBuilder)
            {
                stringBuilder.Append(GetHTML());
            }

        }
        public class MaterialIOTableHead : DOMElement, Renderer
        {
            private DefaultDOMElement tr;
            public List<MaterialIOTableRowHeaderData> TableHeaders = new List<MaterialIOTableRowHeaderData>();

            public MaterialIOTableHeaderRow() : base("thead")
            {
                tr = new DefaultDOMElement("tr");
                tr.AddClass("mdc-data-table__header-row");
            }

            public void AddHeader(MaterialIOTableRowHeaderData header)
            {
                TableHeaders.Add(header);
                tr.AddDOMElement(header);
            }

            public void Output(StringBuilder stringBuilder)
            {
                stringBuilder.Append(GetHTML());
            }

            public override void InsertElement(StringBuilder stringBuilder)
            {
                stringBuilder.Append(tr.GetHTML());
            }

            public class MaterialIOTableRowHeaderData : DOMElement, Renderer
            {
                public string HeaderName { get; set; }
                public MaterialIOTableRowHeaderData() : base("th")
                {
                    AddClass("mdc-data-table__header-cell");
                    AddAttribute(new CustomAttribute("role", "columnheader"));
                    AddAttribute(new CustomAttribute("scope", "col"));
                    Text = HeaderName;
                }

                public override void InsertElement(StringBuilder stringBuilder)
                {
                }

                public void Output(StringBuilder stringBuilder)
                {
                    stringBuilder.Append(GetHTML());
                }

            }

        }

        public class DefaultDOMElement : DOMElement
        {

            public List<DOMElement> dOMElements = new List<DOMElement>();

            public DefaultDOMElement(string elementName) : base(elementName)
            {
            }

            public void AddDOMElement(DOMElement element)
            {
                dOMElements.Add(element);
            } 

            public void RemoveDOMElement(DOMElement element)
            {
                dOMElements.Remove(element);
            }


            public override void InsertElement(StringBuilder stringBuilder)
            {
                foreach(DOMElement element in dOMElements)
                {
                    stringBuilder.Append(element.GetHTML());
                }
            }
        }

        public abstract class DOMElement
        {
            public List<string> ClassDescriptor = new List<string>();
            public string Id { get; set; }
            public string ElementName { get; }
            public string Text { get; set; }

            public List<CustomAttribute> CustomsAttributies = new List<CustomAttribute>();

            public DOMElement(string elementName)
            {
                this.ElementName = elementName;
            }


            public void AddClass(string classValue)
            {
                ClassDescriptor.Add(classValue);

            }


            public void AddAttribute(CustomAttribute attribute)
            {
                CustomsAttributies.Add(attribute);
            }


            public void RemoveAttribute(CustomAttribute value)
            {
                CustomsAttributies.Remove(value);
            }

            public void ClearAttribute()
            {
                CustomsAttributies.Clear();
            }

            abstract public void InsertElement(StringBuilder stringBuilder);

            public string GetHTML()
            {
                StringBuilder stringBuilder = new StringBuilder($"<{ElementName} class={String.Join(" ", ClassDescriptor)} ");

                stringBuilder.Append(String.Join(" ", String.Join(" ", CustomsAttributies.Select(x => x.GetHTML()))));
                stringBuilder.Append(">");

                InsertElement(stringBuilder);

                if (String.IsNullOrEmpty(Text))
                {
                    stringBuilder.Append(Text);
                }


                stringBuilder.Append($"</{ElementName}>");

                return stringBuilder.ToString();
            }


        }

        public class CustomAttribute
        {
            public string Name { get; }
            public List<string> Properties = new List<string>();

            public CustomAttribute(string Name)
            {
                this.Name = Name;
            }
            public CustomAttribute(string Name, params string[] properties)
            {
                this.Name = Name;
                Properties.AddRange(properties);
            }

            public void AddProperty(string value)
            {
                Properties.Add(value);
            }


            public void RemoveProperty(string value)
            {
                Properties.Remove(value);
            }

            public void Clear()
            {
                Properties.Clear();
            }

            public string GetHTML()
            {
                StringBuilder stringBuilder = new StringBuilder($@"{Name}=""");

                stringBuilder.Append(String.Join(" ", Properties));

                stringBuilder.Append(@"""");

                return stringBuilder.ToString();
            }

        }

        public interface Renderer
        {
            void Output(StringBuilder stringBuilder);
        }

    }

    

}