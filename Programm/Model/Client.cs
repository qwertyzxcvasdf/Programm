//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Programm.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Client
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string nickname { get; set; }
        public int role { get; set; }
        public int familyID { get; set; }
        public string money { get; set; }
        public int avatar { get; set; }
        public Nullable<int> card { get; set; }
        public int isPremium { get; set; }
    
        public virtual Avatar Avatar1 { get; set; }
        public virtual Card Card1 { get; set; }
        public virtual Premium Premium { get; set; }
        public virtual Role Role1 { get; set; }
    }
}
