﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_Kho.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuanLyKhoEntities : DbContext
    {
        public QuanLyKhoEntities()
            : base("name=QuanLyKhoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountRole> AccountRoles { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Input> Inputs { get; set; }
        public virtual DbSet<InputInfo> InputInfoes { get; set; }
        public virtual DbSet<Out_put> Out_put { get; set; }
        public virtual DbSet<OutputInfo> OutputInfoes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Suplier> Supliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
    }
}
