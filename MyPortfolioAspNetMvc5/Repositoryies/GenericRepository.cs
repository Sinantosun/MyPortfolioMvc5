﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortfolioAspNetMvc5.Models.Entity;
namespace MyPortfolioAspNetMvc5.Repositoryies
{

    public class GenericRepository<T> where T : class, new()
    {
        DbCvEntities _context = new DbCvEntities();

        public List<T> GetList()
        {
            return _context.Set<T>().ToList();
        }
        public void Insert(T t)
        {
            _context.Set<T>().Add(t);
            _context.SaveChanges();
        }
        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
        }
        public void Update(T t)
        {
            _context.SaveChanges();
        }
        public T GetByID(int id)
        {
            return _context.Set<T>().Find(id);
           
        }
    }
}