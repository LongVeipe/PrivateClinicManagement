//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyPhongKham.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class DONTHUOC
    {
        public int MaPhieuKham { get; set; }
        public int MaThuoc { get; set; }
        public Nullable<int> SoLuongKe { get; set; }
        public int MaCachDung { get; set; }
        public Nullable<int> SoLuongBan { get; set; }
        public Nullable<double> DonGia { get; set; }
        public Nullable<double> ThanhTien { get; set; }
    
        public virtual CACHDUNG CACHDUNG { get; set; }
        public virtual PHIEUKHAM PHIEUKHAM { get; set; }
        public virtual THUOC THUOC { get; set; }
    }
}
