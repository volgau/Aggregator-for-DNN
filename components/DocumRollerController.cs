using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Data;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Search;

namespace DocumRoller
{
	public partial class DocumRollerController :  IPortable
	{
		#region Public methods

		/// <summary>
		/// Initializes a new instance of the <see cref="DocumRoller.DocumRollerController"/> class.
		/// </summary>
		public DocumRollerController ()
		{ 

		}
		/// <summary>
		/// Adds a new T object into the database
		/// </summary>
		/// <param name='info'></param>
		public void Add<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Insert (info);
			}
		}
		/// <summary>
		/// Get single object from the database
		/// </summary>
		/// <returns>
		/// The object
		/// </returns>
		/// <param name='itemId'>
		/// Item identifier.
		/// </param>
		public T Get<T> (int itemId) where T: class
		{
			T info;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				info = repo.GetById (itemId);
			}

			return info;
		}
		/// <summary>
		/// Get single object from the database
		/// </summary>
		/// <returns>
		/// The object
		/// </returns>
		/// <param name='itemId'>
		/// Item identifier.
		/// </param>
		/// <param name='scopeId'>
		/// Scope identifier (like moduleId)
		/// </param>
		public T Get<T> (int itemId, int scopeId) where T: class
		{
			T info;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				info = repo.GetById (itemId, scopeId);
			}

			return info;
		}
		/// <summary>
		/// Updates an object already stored in the database
		/// </summary>
		/// <param name='info'>
		/// Info.
		/// </param>
		public void Update<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Update (info);
			}
		}
		/// <summary>
		/// Gets all objects for items matching scopeId
		/// </summary>
		/// <param name='scopeId'>
		/// Scope identifier (like moduleId)
		/// </param>
		/// <returns></returns>
		public IEnumerable<T> GetObjects<T> (int scopeId) where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.Get (scopeId);

				// Without [Scope("ModuleID")] it should be like:
				// infos = repo.Find ("WHERE ModuleID = @0", moduleId);
			}

			return infos;
		}
		/// <summary>
		/// Gets all objects of type T from database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetObjects<T> () where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.Get ();
			}

			return infos;
		}
		/// <summary>
		/// Gets the all objects of type T from result of a dynamic sql query
		/// </summary>
		/// <returns>Enumerable with objects of type T</returns>
		/// <param name="sqlCondition">SQL command condition.</param>
		/// <param name="args">SQL command arguments.</param>
		/// <typeparam name="T">Type of objects.</typeparam>
		public IEnumerable<T> GetObjects<T> (string sqlConditon, params object[] args) where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.Find (sqlConditon, args);
			}

			return infos;
		}
		/// <summary>
		/// Gets the all objects of type T from result of a dynamic sql query
		/// </summary>
		/// <returns>Enumerable with objects of type T</returns>
		/// <param name="cmdType">Type of an SQL command.</param>
		/// <param name="sql">SQL command.</param>
		/// <param name="args">SQL command arguments.</param>
		/// <typeparam name="T">Type of objects.</typeparam>
		public IEnumerable<T> GetObjects<T> (System.Data.CommandType cmdType, string sql, params object[] args) where T: class
		{
			IEnumerable<T> infos = null;

			using (var ctx = DataContext.Instance()) {
				infos = ctx.ExecuteQuery<T> (cmdType, sql, args);
			}

			return infos;
		}
		/// <summary>
		/// Gets one page of objects of type T
		/// </summary>
		/// <param name="scopeId">Scope identifier (like moduleId)</param>
		/// <param name="index">a page index</param>
		/// <param name="size">a page size</param>
		/// <returns>A paged list of T objects</returns>
		public IPagedList<T> GetPage<T> (int scopeId, int index, int size) where T: class
		{
			IPagedList<T> infos;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.GetPage (scopeId, index, size);
			}

			return infos;
		}
		/// <summary>
		/// Gets one page of objects of type T
		/// </summary>
		/// <param name="index">a page index</param>
		/// <param name="size">a page size</param>
		/// <returns>A paged list of T objects</returns>
		public IPagedList<T> GetPage<T> (int index, int size) where T: class
		{
			IPagedList<T> infos;

			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				infos = repo.GetPage (index, size);
			}

			return infos;
		}
		/// <summary>
		/// Delete a given item from the database by instance
		/// </summary>
		/// <param name='info'></param>
		public void Delete<T> (T info) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Delete (info);

			}
		}
		/// <summary>
		/// Delete a given item from the database by ID
		/// </summary>
		/// <param name='itemId'></param>
		public void Delete<T> (int itemId) where T: class
		{
			using (var ctx = DataContext.Instance()) {
				var repo = ctx.GetRepository<T> ();
				repo.Delete (repo.GetById(itemId));
			}
		}
		#endregion

		#region Class-specific members

		// TODO: Implement class-specific controller members here.

		#endregion

		#region ISearchable members

		/// <summary>
		/// Implements the search interface required to allow DNN to index/search the content of your
		/// module
		/// </summary>
		/// <param name="modInfo"></param>
		/// <returns></returns>
		/// 
		/*
		public DotNetNuke.Services.Search.SearchItemInfoCollection GetSearchItems (ModuleInfo modInfo)
		{
			var searchItems = new SearchItemInfoCollection ();
			var infos = GetObjects<DocumRollerInfo> (modInfo.ModuleID);

			foreach (DocumRollerInfo info in infos) {
				searchItems.Add (
					new SearchItemInfo (
					modInfo.ModuleTitle, 
					info.Content, 
					info.CreatedByUser, 
					info.CreatedOnDate,
					modInfo.ModuleID, 
					info.DocumRollerID.ToString (),
					info.Content, 
					"Item=" + info.DocumRollerID.ToString ())
					);
			}

			return searchItems;
		}*/
		#endregion

		#region IPortable members

		/// <summary>
		/// Exports a module to XML
		/// </summary>
		/// <param name="ModuleID">a module ID</param>
		/// <returns>XML string with module representation</returns>
		public string ExportModule (int moduleId)
		{
			var sb = new StringBuilder ();
			var infos = GetObjects<DocumRollerInfo> (moduleId);

			if (infos != null) {
				sb.Append ("<DocumRollers>");
				foreach (var info in infos) {
					sb.Append ("<DocumRoller>");
					sb.Append ("<content>");
					sb.Append (XmlUtils.XMLEncode (info.Content));
					sb.Append ("</content>");
					sb.Append ("</DocumRoller>");
				}
				sb.Append ("</DocumRollers>");
			}

			return sb.ToString ();
		}
		/// <summary>
		/// Imports a module from an XML
		/// </summary>
		/// <param name="ModuleID"></param>
		/// <param name="Content"></param>
		/// <param name="Version"></param>
		/// <param name="UserID"></param>
		public void ImportModule (int ModuleID, string Content, string Version, int UserID)
		{
			var infos = DotNetNuke.Common.Globals.GetContent (Content, "DocumRollers");

			foreach (XmlNode info in infos.SelectNodes("DocumRoller")) {
				var item = new DocumRollerInfo ();
				item.ModuleID = ModuleID;
				item.Content = info.SelectSingleNode ("content").InnerText;
				//item.CreatedByUser = UserID;

				Add<DocumRollerInfo> (item);
			}
		}
		#endregion
	}
}

