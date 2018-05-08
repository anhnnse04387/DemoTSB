using Models.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDAO
    {
        private ThaiSonBacDMSDbContext db = null;
        public CategoryDAO()
        {
            db = new ThaiSonBacDMSDbContext();
        }
        public List<Category> getLstCate()
        {
            return db.Categories.Where(x => x.Status == 1).ToList();
        }
        //get list category by category id
        public List<Category> getLstCateSearch(string value)
        {
            return db.Categories.Where(x => x.Category_ID.Equals(value) && x.Status == 1).ToList();
        }
        //get category object by category id
        public Category getCategoryById(string cateId)
        {
            Category cate = new Category();
            cate = db.Categories.SingleOrDefault(x => x.Category_ID == cateId && x.Status == 1);
            return cate;
        }

        public int categoryCount()
        {
            return db.Categories.Count();
        }
        public Category getCategoryAllStatus(string cateId)
        {
            return db.Categories.SingleOrDefault(x => x.Category_ID == cateId);
        }
        public List<Sub_category> getSubCategory()
        {
            return db.Sub_category.Where(x => x.Status == 1).ToList();
        }
        public List<Sub_category> getSubCategory(string categoryId)
        {
            return db.Sub_category.Where(x => x.Status == 1 && x.Category_ID.Equals(categoryId)).ToList();
        }
    }
}
