-- 1
select b.NoiDung, b.ThoiDiemDangBai
from BaiViet as b
where b.TacGia = N'Lê Quang Chiến'

-- 2
select DISTINCT b.TacGia
from BaiViet as b

-- 3
select b.TieuDe, b.NoiDung, b.ThoiDiemDangBai, b.TacGia
from BaiViet as b
where b.TieuDe like N'%SQL%'

-- 4
select b.TieuDe, b.NoiDung, b.ThoiDiemDangBai, d.TenDanhMuc
from BaiViet as b inner join DanhMuc as d on b.MaDanhMuc = d.MaDanhMuc
where b.TacGia=N'Trần Nguyên Phong'

-- 5
select d.TenDanhMuc, b.TieuDe, b.NoiDung, b.TacGia, b.ThoiDiemDangBai
from BaiViet as b inner join DanhMuc as d on b.MaDanhMuc = d.MaDanhMuc
where b.TacGia like N'Trần%' and MONTH(b.ThoiDiemDangBai)=6 and YEAR(b.ThoiDiemDangBai)=2021

-- 6
select bl.NoiDung, bl.NguoiBinhLuan, bl.ThoiDiemGuiBai
from BaiBinhLuan as bl inner join BaiViet as bv on bl.MaBaiViet=bv.MaBaiViet inner join DanhMuc as d on bv.MaDanhMuc=d.MaDanhMuc
where d.TenDanhMuc=N'Lập trình'
ORDER BY bl.ThoiDiemGuiBai ASC

-- 7
select bv.MaBaiViet, bv.TieuDe, bv.TacGia, count(*) as SoBaiBinhLuan
from BaiViet as bv left join BaiBinhLuan as bl on bv.MaBaiViet=bl.MaBaiViet
group by bv.MaBaiViet, bv.TieuDe, bv.TacGia

-- 8
select d.MaDanhMuc, d.TenDanhMuc, count(*) as SoBaiViet
from DanhMuc as d inner join BaiViet as bv on d.MaDanhMuc=bv.MaDanhMuc
group by d.MaDanhMuc, d.TenDanhMuc
HAVING count(*) > 2
