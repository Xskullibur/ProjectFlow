using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    [ParseChildren(true)]
    public class MaterialIOTableControl: WebControl, IPostBackEventHandler
    {

        private List<MaterialIOTableRowHeaderData> _headers
        {
            get { return ViewState["Headers"] as List<MaterialIOTableRowHeaderData>; }
            set
            {
                ViewState["Headers"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<MaterialIOTableRowHeaderData> Headers
        {
            get => _headers;
            set
            {
                _headers = value;
            }
        }

        private List<MaterialIOTableRow> _rows
        {
            get { return ViewState["Rows"] as List<MaterialIOTableRow>; }
            set
            {
                ViewState["Rows"] = value;
            }
        }

        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<MaterialIOTableRow> Rows
        {
            get => _rows;
            set
            {
                if (_rows == null)
                {
                    _rows = new List<MaterialIOTableRow>();
                }
                _rows.AddRange(value);
            }
        }
        public MaterialIOTable Table = new MaterialIOTable();


        public void SetHeaders(MaterialIOTableRowHeaderData[] headers)
        {
            this.Headers = new List<MaterialIOTableRowHeaderData>(headers);
        }

        public bool IsSelectMode
        {
            get {
                if (ViewState["IsSelected"] == null) return false;
                else return (bool)ViewState["IsSelected"];
            }
            private set
            {
                ViewState["IsSelected"] = value;
            }
        }

        public bool[] SelectedValues
        {
            get
            {
                bool[] selected = new bool[_rows.Count];
                if (IsSelectMode)
                {
                    for (int i = 0; i < selected.Length; i++)
                    {
                        selected[i] = (_rows[i].Datas[0] as MaterialIOTableRowCheckbox).Checked;
                    }
                }
                return selected;
            }
        }

        public void ToSelectMode()
        {
            if (IsSelectMode) return;
            IsSelectMode = true;
            this.Headers.Insert(0, new MaterialIOTableRowHeaderCheckbox());

            foreach(var row in this.Rows)
            {
                row.Datas.Insert(0, new MaterialIOTableRowCheckbox());
            }
            ViewState["Headers"] = this._headers;
        }

        public void ToNormalMode()
        {
            if (!IsSelectMode) return;
            IsSelectMode = false;
            this.Headers.RemoveAt(0);

            foreach (var row in this.Rows)
            {
                row.Datas.RemoveAt(0);
            }
            ViewState["Headers"] = this._headers;
        }

        public void AddRow(string[] values)
        {

            if (_rows == null)
            {
                _rows = new List<MaterialIOTableRow>();
            }

            var row = new MaterialIOTableRow();

            MaterialIOTableRowData[] datas;

            if (IsSelectMode)
            {
                datas = new MaterialIOTableRowData[values.Length + 1];
                datas[0] = new MaterialIOTableRowCheckbox();
            }
            else
            {
                datas = new MaterialIOTableRowData[values.Length];
            }
            int starting = IsSelectMode ? 1 : 0;
            for (int i = starting; i < datas.Length; i++)
            {
                string value = values[i - starting];
                datas[i] = new MaterialIOTableRowData();
                datas[i].Value = value;
            }
            
            row.SetDatas(datas);
            this._rows.Add(row);
            ViewState["Rows"] = this._rows;
        }

        public void RemoveRow(int index)
        {
            this._rows.RemoveAt(index);
            ViewState["Rows"] = this._rows;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "materialio_selection_postback",
                this.ResolveUrl("~/Scripts/ProjectFlow/materialio_selection_postback.js"));
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Name, base.UniqueID);
        }

        protected override void RenderContents(HtmlTextWriter output)
        {

            Table.Head.ClearHeaders();

            if (Headers != null)
            foreach (var header in Headers)
            {
                Table.Head.AddHeader(header);
            }

            Table.Body.ClearRows();

            if(Rows != null)
            for (int i = 0; i < Rows.Count; i++)
            {
                var row = Rows[i];
                Table.Body.AddRow(row);
            }

            output.Write(Table.GetHTML());
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (!IsSelectMode) return;
            JArray array = JArray.Parse(eventArgument);
            if (array.Count != _rows.Count) return;
            int i = 0;
            foreach(var value in array)
            {
                (_rows[i].Datas[0] as MaterialIOTableRowCheckbox).Checked = value.ToObject<bool>();
                i++;
            }
        }
    }

    public class MaterialIOTableHead : DOMElement, Renderer
    {
        private DefaultDOMElement tr;
        public List<MaterialIOTableRowHeaderData> TableHeaders = new List<MaterialIOTableRowHeaderData>();

        public MaterialIOTableHead() : base("thead")
        {
            tr = new DefaultDOMElement("tr");
            tr.AddClass("mdc-data-table__header-row");
        }

        public void ClearHeaders()
        {
            TableHeaders.Clear();
            tr.ClearDOMElements();
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



    }

    [Serializable]
    public class MaterialIOTableRowHeaderCheckbox : MaterialIOTableRowHeaderData
    {
        public MaterialIOTableRowHeaderCheckbox() : base()
        {
            AddClass(MaterialIODataType.CHECKBOX);
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            string checkboxHTML = $@"<div class=""mdc-checkbox mdc-data-table__row-checkbox mdc-checkbox--upgraded mdc-ripple-upgraded mdc-ripple-upgraded--unbounded"" style=""--mdc-ripple-fg-size:24px;--mdc-ripple-fg-scale:1.6666666666666667;--mdc-ripple-left:8px;--mdc-ripple-top:8px;"">
                                             <input type=""checkbox"" class=""mdc-checkbox__native-control autoselect"">
                                    <div class=""mdc-checkbox__background"">
                                      <svg class=""mdc-checkbox__checkmark"" viewBox=""0 0 24 24"">
                                        <path class=""mdc-checkbox__checkmark-path"" fill=""none"" d=""M1.73,12.91 8.1,19.28 22.79,4.59""></path>
                                      </svg>
                                      <div class=""mdc-checkbox__mixedmark""></div>
                                    </div>
                                    <div class=""mdc-checkbox__ripple""></div>
                                  </div>";
            stringBuilder.Append(checkboxHTML);
        }

    }

    [Serializable]
    public class MaterialIOTableRowHeaderData : DOMElement, Renderer
    {

        public string HeaderName
        {
            get => Text; set
            {
                Text = value;
            }
        }
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

    public class MaterialIOTableBody : DOMElement, Renderer
    {

        List<MaterialIOTableRow> _rows = new List<MaterialIOTableRow>();

        public MaterialIOTableBody() : base("tbody")
        {
            AddClass("mdc-data-table__content");
        }

        public void ClearRows()
        {
            _rows.Clear();
        }

        public void AddRow(MaterialIOTableRow row)
        {
            _rows.Add(row);
        }

        public void AddRow(params MaterialIOTableRowData[] values)
        {
            var tr = new MaterialIOTableRow();
            _rows.Add(tr);
            tr.SetDatas(values);
        }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(GetHTML());
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            foreach(var row in _rows)
            {
                stringBuilder.Append(row.GetHTML());
            }
            
        }

    }

    [Serializable]
    [ParseChildren(true)]
    public class MaterialIOTableRow : DOMElement, Renderer
    {
        List<MaterialIOTableRowData> _datas = new List<MaterialIOTableRowData>();
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public List<MaterialIOTableRowData> Datas
        {
            get { return _datas; }
        }

        public MaterialIOTableRow() : base("tr")
        {
           AddClass("mdc-data-table__row");
        }

        public void SetDatas(params MaterialIOTableRowData[] values)
        {
            _datas.Clear();
            _datas.AddRange(values);
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            foreach (var data in _datas)
            {
                stringBuilder.Append(data.GetHTML());
            }
        }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(GetHTML());
        }
    }
    
    [Serializable]
    public class MaterialIOTableRowCheckbox : MaterialIOTableRowData
    {
        public bool Checked { get; set; }

        public MaterialIOTableRowCheckbox()
        {
            AddClass(MaterialIODataType.CHECKBOX);
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            string checkboxHTML = $@"<div class=""mdc-checkbox mdc-data-table__row-checkbox mdc-checkbox--upgraded mdc-ripple-upgraded mdc-ripple-upgraded--unbounded"" style=""--mdc-ripple-fg-size:24px;--mdc-ripple-fg-scale:1.6666666666666667;--mdc-ripple-left:8px;--mdc-ripple-top:8px;"">
                                             <input type=""checkbox"" class=""mdc-checkbox__native-control"" {(Checked ? "checked" : "")}>
                                    <div class=""mdc-checkbox__background"">
                                      <svg class=""mdc-checkbox__checkmark"" viewBox=""0 0 24 24"">
                                        <path class=""mdc-checkbox__checkmark-path"" fill=""none"" d=""M1.73,12.91 8.1,19.28 22.79,4.59""></path>
                                      </svg>
                                      <div class=""mdc-checkbox__mixedmark""></div>
                                    </div>
                                    <div class=""mdc-checkbox__ripple""></div>
                                  </div>";
            stringBuilder.Append(checkboxHTML);
        }
    }

    [Serializable]
    public class MaterialIOTableRowData : DOMElement, Renderer
    {

        public string Value { get => Text; set
            {
                Text = value;
            }
        }

        public MaterialIOTableRowData() : base("td")
        {
            AddClass("mdc-data-table__cell");
        }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(GetHTML());
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            
        }
    }

    public class MaterialIODataType
    {
        public static string CHECKBOX = "mdc-data-table__cell--checkbox";
        public static string NUMERIC = "mdc-data-table__cell--numeric";

    }

    public class MaterialIOTable : DOMElement, Renderer
    {
        private DefaultDOMElement Table;
        public MaterialIOTableHead Head = new MaterialIOTableHead();
        public MaterialIOTableBody Body = new MaterialIOTableBody();

        public MaterialIOTable() : base("div")
        {
            AddClass("w-100");
            AddClass("mdc-data-table");
            Table = new DefaultDOMElement("table");
            Table.AddClass("mdc-data-table__table");
            Table.AddDOMElement(Head);
            Table.AddDOMElement(Body);
        }

        public override void InsertElement(StringBuilder stringBuilder)
        {
            stringBuilder.Append(Table.GetHTML());
        }

        public void Output(StringBuilder stringBuilder)
        {
            stringBuilder.Append(GetHTML());
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

        public void AddDOMElementRange(IEnumerable<DOMElement> elements)
        {
            dOMElements.AddRange(elements);
        }

        public void RemoveDOMElement(DOMElement element)
        {
            dOMElements.Remove(element);
        }


        public override void InsertElement(StringBuilder stringBuilder)
        {
            foreach (DOMElement element in dOMElements)
            {
                stringBuilder.Append(element.GetHTML());
            }
        }

        public void ClearDOMElements()
        {
            dOMElements.Clear();
        }
    }
    [Serializable]
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
            StringBuilder stringBuilder = new StringBuilder($@"<{ElementName} class=""{String.Join(" ", ClassDescriptor)}""");

            stringBuilder.Append(String.Join(" ", String.Join(" ", CustomsAttributies.Select(x => x.GetHTML()))));
            stringBuilder.Append(">");

            InsertElement(stringBuilder);

            if (!String.IsNullOrEmpty(Text))
            {
                stringBuilder.Append(Text);
            }


            stringBuilder.Append($"</{ElementName}>");

            return stringBuilder.ToString();
        }

        [Serializable]
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

        

    }

    public interface Renderer
    {
        void Output(StringBuilder stringBuilder);
    }

}