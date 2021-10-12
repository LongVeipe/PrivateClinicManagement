create database QuanLyPhongKhamTu
go

use QuanLyPhongKhamTu
go

create table DONVI
(
	MaDonVi int identity(1,1) primary key,
	TenDonVi nvarchar(100),
	DaXoa bit default 0
)
go

create table THUOC
(
	MaThuoc int identity(1,1) primary key,
	TenThuoc nvarchar(max),
	MaDonVi int not null,
	DonGia float default 0,
	SoLuongTon int default 0,
	DaXoa bit default 0
)
go

alter table THUOC add constraint MaDonVi_FK foreign key(MaDonVi) references DONVI(MaDonVi)
go

create table BENHNHAN
(
	MaBenhNhan int identity(1,1) primary key,
	TenBenhNhan nvarchar(100),
	DiaChi nvarchar(max),
	SoDienThoai nvarchar(20),
	NgaySinh datetime,
	GioiTinh nvarchar(10)
)
go

create table LOAIBENH
(
	MaLoaiBenh int identity(1,1) primary key,
	TenLoaiBenh nvarchar(max),
	DaXoa bit default 0
)
go

create table PHIEUKHAM
(
	MaPhieuKham int identity(1,1) primary key,
	NgayKham datetime default GETDATE(),
	MaBenhNhan int not null,
	TrieuChung nvarchar(100),
	MaLoaiBenh int not null default 1,
	NgayTaiKham datetime,
	DaBanThuoc bit default 0
)
go

alter table PHIEUKHAM add constraint MaBenhNhan_FK foreign key(MaBenhNhan) references BENHNHAN(MaBenhNhan)
go
alter table PHIEUKHAM add constraint MaLoaiBenh_FK foreign key(MaLoaiBenh) references LOAIBENH(MaLoaiBenh)
go

create table CACHDUNG
(
	MaCachDung int identity(1,1) primary key,
	TenCachDung nvarchar(max),
	DaXoa bit default 0
)
go

create table DONTHUOC
(
	MaPhieuKham int not null,
	MaThuoc int not null,
	SoLuongKe int default 0,
	MaCachDung int not null,
	SoLuongBan int default 0,
	DonGia float default 0,
	ThanhTien float default 0
)
go

alter table DONTHUOC add constraint DONTHUOC_PK primary key (MaPhieuKham,MaThuoc)
go

alter table DONTHUOC add constraint MaPhieuKham_DONTHUOC_FK foreign key (MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

alter table DONTHUOC add constraint MaThuoc_DONTHUOC_FK foreign key (MaThuoc) references THUOC(MaThuoc)
go

alter table DONTHUOC add constraint MaCachDung_DONTHUOC_FK foreign key (MaCachDung) references CACHDUNG(MaCachDung)
go

create table PHIEUNHAP
(
	MaPhieuNhap int identity(1,1) primary key,
	NgayNhap datetime default GETDATE(),
	TongTien float default 0,
	GhiChu nvarchar(max)
)
go

create table CHITIETPHIEUNHAP
(
	MaPhieuNhap int not null,
	MaThuoc int not null,
	SoLuong int default 0,
	GiaNhap float default 0,
	ThanhTien float default 0
)
go

alter table CHITIETPHIEUNHAP add constraint CHITIETPHIEUNHAP_PK primary key(MaPhieuNhap,MaThuoc)
go

alter table CHITIETPHIEUNHAP add constraint MaPhieuNhap_CHITIETPHIEUNHAP_FK foreign key(MaPhieuNhap) references PHIEUNHAP(MaPhieuNhap)
go

alter table CHITIETPHIEUNHAP add constraint MaThuoc_CHITIETPHIEUNHAP_FK foreign key(MaThuoc) references THUOC(MaThuoc)
go

create table DICHVU
(
	MaDichVu int identity(1,1) primary key,
	TenDichVu nvarchar(max),
	DonGia float default 0,
	DaXoa bit default 0
)
go

create table CHITIETDICHVU
(
	MaPhieuKham int not null,
	MaDichVu int not null,
	DonGia float default 0
)
go

alter table CHITIETDICHVU add constraint CHITIETDICHVU_PK primary key (MaPhieuKham,MaDichVu)
go

alter table CHITIETDICHVU add constraint MaPhieuKham_CHITIETDICHVU foreign key (MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

alter table CHITIETDICHVU add constraint MaDichVu_CHITIETDICHVU foreign key (MaDichVu) references DICHVU(MaDichVu)
go

create table HOADON
(
	MaHoaDon int identity(1,1) primary key,
	MaPhieuKham int not null,
	TienKhamBenh float default 0,
	TienThuoc float default 0,
	TienDichVu float default 0,
	TongTien float default 0,
)
go

alter table HOADON add constraint MaPhieuKham_HOADON_FK foreign key (MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

create table THAMSO
(
	Id int primary key identity(1,1),
	SoLuongBenhNhanToiDaTrongNgay int,
	TienKhamBenh float
)
go

create table HANGCHOKHAMBENH
(
	MaPhieuKham int primary key,
	TimeInQueue DateTime default GETDATE()
)
go

alter table HANGCHOKHAMBENH add constraint MaPhieuKham_HANGCHOKHAMBENH_FK foreign key(MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

create table HANGCHOTHANHTOAN
(
	MaPhieuKham int not null,
	MaDichVu int not null,
	TimeInQueue DateTime default GETDATE()
)
go

alter table HANGCHOTHANHTOAN add constraint HANGCHOTHANHTOAN_PK primary key (MaPhieuKham,MaDichVu)
go

alter table HANGCHOTHANHTOAN add constraint MaPhieuKham_HANGCHOTHANHTOAN_FK foreign key(MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

alter table HANGCHOTHANHTOAN add constraint MaDichVu_HANGCHOTHANHTOAN_FK foreign key(MaDichVu) references DICHVU(MaDichVu)
go

create table HANGSUDUNGDICHVU
(
	MaPhieuKham int not null,
	MaDichVu int not null,
	TimeInQueue DateTime default GETDATE()
)
go

alter table HANGSUDUNGDICHVU add constraint HANGSUDUNGDICHVU_PK primary key (MaPhieuKham,MaDichVu)
go

alter table HANGSUDUNGDICHVU add constraint MaPhieuKham_HANGSUDUNGDICHVU_FK foreign key(MaPhieuKham) references PHIEUKHAM(MaPhieuKham)
go

alter table HANGSUDUNGDICHVU add constraint MaDichVu_HANGSUDUNGDICHVU_FK foreign key(MaDichVu) references DICHVU(MaDichVu)
go

create table BAOCAOSUDUNGTHUOC
(
	Thang int not null,
	Nam int not null,
	MaThuoc int not null,
	SoLuong int,
	SoLanDung int
)
go

alter table BAOCAOSUDUNGTHUOC add constraint BAOCAOSUDUNGTHUOC_PK primary key (Thang,Nam,MaThuoc)
go

alter table BAOCAOSUDUNGTHUOC add constraint MaThuoc_BAOCAOSUDUNGTHUOC_FK foreign key (MaThuoc) references THUOC(MaThuoc)
go

create table BAOCAODOANHTHUTHANG
(
	MaBaoCaoDoanhThuThang int identity(1,1) primary key,
	Thang int,
	Nam int,
	DoanhThu float
)
go

create table CHITIETBAOCAODOANHTHUTHANG
(
	MaBaoCaoDoanhThuThang int not null,
	Ngay int not null,
	SoBenhNhan int,
	DoanhThu float,
	TyLe float
)
go

alter table CHITIETBAOCAODOANHTHUTHANG add constraint CHITIETBAOCAODOANHTHUTHANG_PK primary key(MaBaoCaoDoanhThuThang,Ngay)
go

alter table CHITIETBAOCAODOANHTHUTHANG add constraint MaBaoCaoDoanhThuThang_CHITIETBAOCAODOANHTHUTHANG_FK foreign key(MaBaoCaoDoanhThuThang) references BAOCAODOANHTHUTHANG(MaBaoCaoDoanhThuThang)
go

create table NGUOIDUNG
(
	TenDangNhap nvarchar(100) primary key,
	MatKhau nvarchar(100),
	TenNguoiDung nvarchar(100),
	DiaChi nvarchar(max),
	SoDienThoai nvarchar(20),
	NgaySinh datetime,
	GioiTinh nvarchar(10),
	Email nvarchar(max),
	CMND nvarchar(50)
)
go

insert into NGUOIDUNG(TenDangNhap,MatKhau,TenNguoiDung,DiaChi,SoDienThoai,NgaySinh,GioiTinh,Email,CMND) values (N'admin',N'admin',N'Quản trị viên',N'Bình Định',N'0376277843','9/4/1999',N'Nam',N'quanquya2@gmail.com',N'215469380')
go

insert into LOAIBENH(TenLoaiBenh) values (N'Chưa rõ')
go

insert into THAMSO(SoLuongBenhNhanToiDaTrongNgay,TienKhamBenh) values (40,100000)
go