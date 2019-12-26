using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.dao
{
    /// <summary>
    /// thao tác với bảng mst_japan trong db
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class MstJapanDao
    {
        TrainingDbContext _db = null;
        /// <summary>
        /// contructor
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        public MstJapanDao()
        {
            _db = new TrainingDbContext();
        }
        /// <summary>
        /// lấy tất cả trình độ tiếng nhật trong DB
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public List<mst_japan> getAllLevel()
        {
            return _db.mst_japan.ToList();
        }
        /// <summary>
        /// lấy codeLevel từ NameLeve
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="nameLevel"></param>
        /// <returns></returns>
        public string getCodeLevel(string nameLevel)
        {
            return _db.mst_japan.Where(x => x.name_level.Equals(nameLevel)).Select(x => x.code_level).FirstOrDefault();
        }
    }
}
