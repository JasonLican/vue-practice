using ICan.Module.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.EFDbContext
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<SysMenus> Menus { get; set; }
        public DbSet<SysRoles> Roles { get; set; }
        public DbSet<SysRoleMenus> SysRoleMenus { get; set; }
        public DbSet<SysUser> Users { get; set; }
        public DbSet<V_User_Role> V_User_Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SysUser>(entity =>
            {
                entity.ToTable("Sys_User");
            });
            modelBuilder.Entity<SysMenus>(entity=>{
                entity.ToTable("SysMenus");
            });
            modelBuilder.Entity<SysRoles>(entity => { entity.ToTable("SysRoles"); });
            modelBuilder.Entity<SysRoleMenus>(entity => { entity.ToTable("SysRoleMenus"); });
            modelBuilder.Entity<V_User_Role>(entity => { entity.HasNoKey(); entity.ToView("v_user_menu"); });
        }
    }
}
