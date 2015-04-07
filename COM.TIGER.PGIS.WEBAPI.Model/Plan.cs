using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM.TIGER.PGIS.WEBAPI.Model
{
    /// <summary>
    /// 预案信息
    /// </summary>
    [System.Runtime.Serialization.DataContract(Name = "plan", Namespace = "http://www.tigerhz.com/web/api/model/")]
    public class Plan:MBase,IComparable<Plan>
    {
        /// <summary>
        /// 标识符
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "ID")]
        public int ID { get; set; }

        /// <summary>
        /// 预案名称
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Name")]
        public string Name { get; set; }

        /// <summary>
        /// 预案描述信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Description")]
        public string Description { get; set; }
        
        private List<Tag> _tags = new List<Tag>();
        /// <summary>
        /// 预案标注信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Tags")]
        public Tag[] Tags
        {
            get 
            {
                _tags.Sort();
                var items = new Tag[_tags.Count];
                _tags.CopyTo(items);
                return items;
            }
        }

        private List<File> _files = new List<File>();
        /// <summary>
        /// 预案文件信息
        /// </summary>
        [System.Runtime.Serialization.DataMember(Name = "Files")]
        public File[] Files
        {
            get
            {
                _files.Sort();
                var items = new File[_files.Count];
                _files.CopyTo(items);                
                return items;
            }
        }

        public File this[int index]
        {
            get { return _files[index]; }
        }

        public File this[string name]
        {
            get { return _files.FirstOrDefault(t => t.Name == name); }
        }

        /// <summary>
        /// 添加预案标注信息
        /// </summary>
        /// <param name="tag"></param>
        public void Add(Tag tag)
        {
            if (_tags.Exists(t => t.Equals(tag))) return;
            _tags.Add(tag);
        }

        /// <summary>
        /// 添加预案文档信息
        /// </summary>
        /// <param name="file"></param>
        public void Add(File file)
        {
            if (_files.Exists(t => t.Equals(file))) return;
            _files.Add(file);
        }

        /// <summary>
        /// 批量添加预案标注信息
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<Tag> items)
        {
            items = items.Where(x => !_tags.Exists(t => t.Equals(x)));
            _tags.AddRange(items);
        }

        /// <summary>
        /// 批量添加预案文档信息
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<File> items)
        {
            items = items.Where(x => !_files.Exists(t => t.Equals(x)));
            _files.AddRange(items);
        }

        public int CompareTo(Plan other)
        {
            if (ID > other.ID) return 1;
            if (ID == other.ID) return 0;
            return -1;
        }

        public override bool Equals(object obj)
        {
            var other = obj as Plan;
            if (other == null) return false;
            return ID == other.ID
                && Name == other.Name
                && Description == other.Description;
        }
    }
}
