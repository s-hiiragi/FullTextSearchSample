using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FullTextSeachSample
{
    /// <summary>
    /// 拡張子フィルタクラス
    /// <c>SelectExtensionsDialog</c>の<c>ListView</c>の項目と対応します。
    /// </summary>
    public class ExtensionFilter
    {
        #region プロパティ

        /// <summary>
        /// フィルタが有効か？
        /// <c>ListViewItem</c>のチェックボックスと対応します。
        /// </summary>
        public bool Enabled { get; set; }

        public IEnumerable<string> Extensions { get; set; }

        public string Description { get; set; }

        public string ExtensionsString
        {
            get
            {
                return String.Join("|", Extensions);
            }
        }

        #endregion

        #region コンストラクタ

        public ExtensionFilter()
        {
            Enabled = false;
            Extensions = StringUtils.EmptyCollection;
            Description = String.Empty;
        }

        public ExtensionFilter(bool enabled, IEnumerable<string> extensions, string description)
        {
            Enabled = enabled;
            Extensions = extensions;
            Description = description;
        }

        /// <summary>
        /// <c>ListViewItem</c>からの作成が若干容易。
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="extensions"></param>
        /// <param name="description"></param>
        public ExtensionFilter(bool enabled, string extensions, string description)
        {
            Enabled = enabled;
            Extensions = extensions.Split('|');
            Description = description;
        }

        #endregion
    }

    /// <summary>
    /// 拡張子フィルタ列クラス
    /// <c>SelectExtensionsDialog</c>の<c>ListView</c>のデータと対応します。
    /// </summary>
    public class ExtensionFilters
    {
        #region プロパティ

        private readonly List<ExtensionFilter> filters
            = new List<ExtensionFilter>();

        public IEnumerable<ExtensionFilter> Filters
        {
            get
            {
                return filters;
            }
            set
            {
                filters.Clear();
                filters.AddRange(value);
            }
        }

        /// <summary>
        /// フィルタを通過する拡張子列を返します。
        /// </summary>
        public IEnumerable<string> FilteredExtensions
        {
            get
            {
                return from f in filters
                    where f.Enabled
                    select f.Extensions into exts
                    from ext in exts
                    select ext;
            }
        }

        #endregion

        #region シリアライズ

        /// <summary>
        /// ApplicationSettingsで保存するための形式に変換します。
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> Serialize()
        {
            return Filters
                .Select(f => String.Join(
                    ",", f.Enabled.ToString(), String.Join("|", f.Extensions), f.Description)
                );
        }

        /// <summary>
        /// ApplicationSettingsから取得したフィルタ文字列のリストをフィルタ列に戻します。
        /// </summary>
        /// <param name="filterStrings"></param>
        public static ExtensionFilters Deserialize(IEnumerable<string> filterStrings)
        {
            var filters = from s in filterStrings
                    select s.Split(new[] { ',' }, 3) into values
                    select new ExtensionFilter(Boolean.Parse(values[0]), values[1], values[2]);

            return new ExtensionFilters(filters);
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ExtensionFilters()
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="extensionFilters"></param>
        public ExtensionFilters(IEnumerable<ExtensionFilter> extensionFilters)
        {
            Filters = extensionFilters;
        }

        #endregion

        #region インスタンスメソッド

        /// <summary>
        /// オブジェクトを複製
        /// 各要素(<c>ExtensionFilter</c>)は複製しません。
        /// </summary>
        /// <returns></returns>
        public ExtensionFilters Clone()
        {
            return new ExtensionFilters(filters);
        }

        #endregion
    }
}
