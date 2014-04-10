using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SchneiderMilkManagement.BusinessLayer.BusinessFacade;

namespace SchneiderMilkManagement
{
    public class BaseGridPage : BasePage
    {
        #region Variables

        private GridView _gvItems;
        private ObjectDataSource _odsItems;
        private LinkButton _lnkCheckAll;
        private LinkButton _lnkClearAll;
        private LinkButton _lnkDeleteChecked;
        private HtmlGenericControl _spanGridOptions;
        private Label _lblError;
        private IFacade _objFacade;
        private string[] SeperatorString = { "," };
        #endregion
        
        public BaseGridPage()
        {
            _gvItems = null;
        }

        public string SearchString
        {
            get
            {
                if (ViewState["SearchString"] != null)
                {
                    return ViewState["SearchString"].ToString();
                }
                else
                {
                    return " 1=1";
                }
            }
            set
            {
                ViewState["SearchString"] = value;
            }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            ErrorMessage = "";
            base.OnLoad(e);
        }

        protected override void OnInit(EventArgs e)
        {
            if (GvItems != null)
            {
                if (GvItems.AllowSorting == true)
                {
                    GvItems.Sorting += new GridViewSortEventHandler(GvItems_Sorting);
                }

                if (GvItems.AllowPaging == true)
                {
                    GvItems.PagerStyle.HorizontalAlign = HorizontalAlign.Right;
                    GvItems.PagerSettings.Mode = PagerButtons.NumericFirstLast;
                    GvItems.PagerSettings.FirstPageText = "First Page";
                    GvItems.PagerSettings.LastPageText = "Last Page";
                    if (OdsItems == null)
                    {
                        GvItems.PageIndexChanging += new GridViewPageEventHandler(GvItems_Paging);
                    }
                }

                GvItems.RowCreated += new GridViewRowEventHandler(GvItems_RowCreated);
                GvItems.PreRender += new EventHandler(GvItems_PreRender);
                if (LnkDeleteChecked != null)
                {
                    LnkDeleteChecked.Click += new EventHandler(LnkDeleteChecked_Click);
                }
            }

            base.OnInit(e);
        }

        protected virtual void BindGrid()
        {

        }

        public string FormatFilterString(string filterString)
        {
            filterString = filterString.Replace("'", "''");

            return filterString;
        }


        #region Properties

        /// <summary>
        /// Gets or sets the reference of the grid view.
        /// </summary>
        protected GridView GvItems
        {
            get { return _gvItems; }
            set { _gvItems = value; }
        }

        /// <summary>
        /// Gets or sets the reference of the grid view.
        /// </summary>
        protected ObjectDataSource OdsItems
        {
            get { return _odsItems; }
            set { _odsItems = value; }
        }

        /// <summary>
        /// Gets or sets the current column being sorted on.
        /// </summary>
        protected string SortColumn
        {
            get { return Convert.ToString(ViewState[ClientID + "SortColumn"]); }
            set { ViewState[ClientID + "SortColumn"] = value; }
        }

        /// <summary>
        ///  Gets or sets the current sort direction (ascending or descending).
        /// </summary>
        protected string SortOrder
        {
            get { return Convert.ToString(ViewState[ClientID + "SortOrder"]); }
            set { ViewState[ClientID + "SortOrder"] = value; }
        }

        /// <summary>
        /// Gets the Sql sort expression for the current sort settings.
        /// </summary>
        protected string SortExpression
        {
            get { return SortColumn + " " + SortOrder; }
        }

        /// <summary>
        /// Gets or sets the filter value.
        /// </summary>
        protected string FilterValue
        {
            get { return Convert.ToString(ViewState[ClientID + "FilterValue"]); }
            set { ViewState[ClientID + "FilterValue"] = value; }
        }

        /// <summary>
        /// Gets or set to check all.
        /// </summary>
        protected LinkButton LnkCheckAll
        {
            get { return _lnkCheckAll; }
            set { _lnkCheckAll = value; }
        }

        /// <summary>
        /// Gets or set to clear all.
        /// </summary>
        protected LinkButton LnkClearAll
        {
            get { return _lnkClearAll; }
            set { _lnkClearAll = value; }
        }

        /// <summary>
        /// Gets or set to selected delete.
        /// </summary>
        protected LinkButton LnkDeleteChecked
        {
            get { return _lnkDeleteChecked; }
            set { _lnkDeleteChecked = value; }
        }


        /// <summary>
        /// Gets or set to Span Grid Options.
        /// </summary>
        protected HtmlGenericControl SpanGridOptions
        {
            get { return _spanGridOptions; }
            set { _spanGridOptions = value; }
        }
        /// <summary>
        /// Gets or set error message text.
        /// </summary>
        protected Label LblError
        {
            get { return _lblError; }
            set { _lblError = value; }
        }

        /// <summary>
        /// Gets or set to IFacade object.
        /// </summary>

        protected IFacade ObjIFacade
        {
            get { return _objFacade; }
            set { _objFacade = value; }
        }

        /// <summary>
        /// set error message text.
        /// </summary>
        protected string ErrorMessage
        {
            set
            {
                if (LblError != null)
                    LblError.Text = value;
            }
        }


        #endregion

        #region Event Handlers

        /// <summary>
        /// Sets sort order and re-binds grid view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GvItems_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression.ToLower().Trim() == SortColumn.ToLower())
            {
                SortOrder = (SortOrder == "ASC") ? "DESC" : "ASC";
            }
            else
            {
                SortOrder = "ASC";
                SortColumn = e.SortExpression.Trim();
            }

            if (OdsItems == null)
            {
                BindGrid();
            }
            else
            {
                OdsItems.SelectParameters[0].DefaultValue = SortExpression;
                e.Cancel = true;
                GvItems.DataBind();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GvItems_Paging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GvItems.PageIndex = e.NewPageIndex;
            }
            catch
            {
                GvItems.PageIndex = 0;
            }

            BindGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GvItems_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (GvItems.AllowSorting == true)
                {
                    for (int index = 0; index < GvItems.Columns.Count; index++)
                    {
                        DataControlField gvColumn = GvItems.Columns[index];

                        if (gvColumn.SortExpression != null)
                        {
                            if (gvColumn.SortExpression.ToLower().Trim() == SortColumn.ToLower())
                            {
                                Literal litGap = new Literal();
                                litGap.Text = "&nbsp;";
                                e.Row.Cells[index].Controls.Add(litGap);

                                Image imgSort = new Image();
                                imgSort.ImageAlign = ImageAlign.AbsMiddle;
                                imgSort.ImageUrl = (SortOrder == "DESC") ? "~/images/downarrow.gif" : "~/images/uparrow.gif";
                                e.Row.Cells[index].Controls.Add(imgSort);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void GvItems_PreRender(object sender, EventArgs e)
        {

            if (GvItems != null)
            {
                if (GvItems.Rows.Count > 0)
                {
                    if (SpanGridOptions != null)
                    {
                        SpanGridOptions.Visible = true;
                    }

                    if (LnkDeleteChecked != null)
                    {
                        LnkDeleteChecked.OnClientClick = "return Check_Delete('" + GvItems.ClientID + "'," + GvItems.Rows.Count + ",'" + _lblError.ClientID + "');";
                    }

                    if (LnkCheckAll != null)
                    {
                        LnkCheckAll.OnClientClick = "return CheckAll('" + GvItems.ClientID + "'," + GvItems.Rows.Count + ");";
                    }

                    if (LnkClearAll != null)
                    {
                        LnkClearAll.OnClientClick = "return ClearAll('" + GvItems.ClientID + "'," + GvItems.Rows.Count + ");";
                    }
                }
                else
                {
                    if (SpanGridOptions != null)
                    {
                        SpanGridOptions.Visible = false;
                    }
                }
            }
            else
            {
                if (SpanGridOptions != null)
                {
                    SpanGridOptions.Visible = false;
                }
            }
        }

        protected virtual bool ValidateFunction()
        {
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void LnkDeleteChecked_Click(object sender, EventArgs e)
        {

            if (ValidateFunction())
            {
                string itemIds = SelectedGridItemIds();
                int delStatus = 0;
                if (ObjIFacade != null && itemIds.Length > 0)
                {
                    delStatus = ObjIFacade.DeleteWithArray(itemIds);

                    if (delStatus > 0)
                    {
                        ErrorMessage = "Successfully deleted the selected items";
                    }
                    else
                    {
                        ErrorMessage = "Delete option is not available for selected record(s).";
                    }
                    if (itemIds.Split(SeperatorString, StringSplitOptions.RemoveEmptyEntries).Length == GvItems.Rows.Count)
                    {
                        GvItems.PageIndex = (GvItems.PageIndex == 0) ? 0 : GvItems.PageIndex - 1;
                    }

                    if (OdsItems == null)
                    {
                        BindGrid();
                    }
                    else
                    {
                        GvItems.DataBind();
                    }
                }
            }
        }

        protected string SelectedGridItemIds()
        {
            string itemIds = "";

            foreach (GridViewRow gvRow in GvItems.Rows)
            {
                if (gvRow.FindControl("chkDel") != null)
                {
                    if (((CheckBox)(gvRow.FindControl("chkDel"))).Checked == true)
                    {
                        itemIds += SeperatorString[0] + GvItems.DataKeys[gvRow.RowIndex].Value.ToString();
                    }
                }
            }
            if (itemIds.Length > 0)
            {
                itemIds = itemIds.Substring(1);
            }
            return itemIds;
        }

        #endregion
    }
}
