using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryService
{
    public class UserQueryService
    {
        public User GetUserByUserName(string userName)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                return _dbContext.User.FirstOrDefault(t => t.UserName == userName);
            }
        }

        public List<BorrowRecordView> GetBorrowRecordsByUserId(Guid userId)
        {
            using (QueryDBEntities _dbContext = new QueryDBEntities())
            {
                return _dbContext.BorrowRecordView.Where(t => t.UserAggregateRootId == userId).OrderByDescending(t => t.Id).ToList();
            }
        }
    }
}
