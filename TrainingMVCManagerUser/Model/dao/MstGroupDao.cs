using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.dao
{
    /// <summary>
    /// thao tác với bảng mst_group
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public class MstGroupDao
    {
        TrainingDbContext _db = null;
        /// <summary>
        /// contructor
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        public MstGroupDao()
        {
            _db = new TrainingDbContext();
        }
        /// <summary>
        /// lấy danh sách group trong db
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <returns></returns>
        public List<mst_group> getAllGroup()
        {
            return _db.mst_group.ToList();
        }
        /// <summary>
        /// kiểm tra tồn tài của 1 groupId
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool CheckExistedGroup(string groupId)
        {
            int count = _db.mst_group.Where(x => x.group_id.ToString().Equals(groupId)).Select(x => x).Count();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// lấy group name từ groupId
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public string GetGroupName(string groupId)
        {
            return _db.mst_group.Where(x => x.group_id.ToString().Equals(groupId)).Select(x => x.group_name).FirstOrDefault();
        }
    }
}
