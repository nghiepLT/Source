using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserMng.BLC;
using UserMng.DAC;
using PQT.DAC.ViewModel;
using PQT.DAC;
namespace WebCus
{
    public partial class Khaosathoinhap : System.Web.UI.Page
    {
        UserMngOther_BLC blc_user = new UserMngOther_BLC();
        UserMng_DAC nDAC = new UserMng_DAC();
        protected Guid IDNTD
        {
            get
            {
                if (ViewState["g_IDNTD"] != null)
                    return Guid.Parse(ViewState["g_IDNTD"].ToString());
                return new Guid();
            }
            set
            {
                ViewState["g_IDNTD"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if (Request.QueryString["id"] != null)
                {
                    this.IDNTD = Guid.Parse(Request.QueryString["id"]);
                    btnSaveBanner.Visible = false;
                   
                    UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
                    Ungvienkhaosat uvks = blc_user.GetUngvienkhaosat(uv.IdNhanVien.Value);
                    if(uvks!=null)
                    this.step.Value = uvks.Step.Value.ToString();
                }
                else
                {
                    TUser tuser = blc_user.GetUser_ByIDAll(UserMemberID);
                    if (tuser != null)
                    {
                        ThongTinNhanSu ttns = blc_user.GetTTNhansu_byID(tuser.IdNhansu.Value);
                        this.spngaynhanviec.InnerText = ttns.NgayVaoLam.Value.Day.ToString();
                        this.spthangnhanviec.InnerText = ttns.NgayVaoLam.Value.Month.ToString();
                        this.spnamnhanviec.InnerText = ttns.NgayVaoLam.Value.Year.ToString();
                        this.spngayhientai.InnerText = DateTime.Now.Day.ToString();
                        this.spthanghientai.InnerText = DateTime.Now.Month.ToString();
                        this.spnamghientai.InnerText = DateTime.Now.Year.ToString();
                    }
                    if (tuser != null)
                    {
                        Ungvienkhaosat uvks = blc_user.GetUngvienkhaosat(tuser.IdNhansu.Value);
                        if (uvks == null)
                        {
                            this.atab2.Visible = false;
                            this.atab3.Visible = false;
                            this.songay.InnerText = "7 Ngày";

                            this.type.Value = "1";
                        }
                        else
                        {
                            if (uvks.Step == 1)
                            {
                                this.atab2.Visible = false;
                                this.atab3.Visible = false;
                                this.songay.InnerText = "7 Ngày";
                                this.type.Value = "1";
                            }
                            if (uvks.Step == 2)
                            {
                                this.atab1.Visible = false;
                                this.atab3.Visible = false;
                                this.songay.InnerText = "14 Ngày";
                                this.type.Value = "2";
                            }
                            if (uvks.Step == 3)
                            {
                                this.atab1.Visible = false;
                                this.atab2.Visible = false;
                                this.songay.InnerText = "2 tháng";
                                this.type.Value = "3";
                            }
                        }
                    }
                }
                int selectid = 0;
                if (this.IDNTD == Guid.Empty)
                {
                    selectid = UserMemberID;
                }
                else
                {
                    UngVien uv = blc_user.GetUngVienByID(this.IDNTD);
                    //Giờ làm việc
                    TUser tus = blc_user.GetUserByNhanvienId(uv.IdNhanVien.Value);
                    selectid = tus.UserID;
                }
                TUser user = blc_user.GetUser_ByIDAll(selectid);
                if (user != null)
                {
                    var ungvien = blc_user.GetUngVienByIDNV(user.IdNhansu.Value);
                    if (ungvien != null)
                    {
                        if (ungvien != null)
                        {
                            this.hoten.Value = ungvien.HoTen;
                            if (!string.IsNullOrEmpty(ungvien.UngCuViTri))
                            {
                                ThongTinViTri ThongTinViTri = JsonConvert.DeserializeObject<ThongTinViTri>(ungvien.UngCuViTri);
                                this.vitri.Value = ThongTinViTri.ViTriungTuyen;
                            }
                        }
                        //Load data 7ngay
                        Ungvienkhaosat Ungvienkhaosat = blc_user.GetUngvienkhaosat(ungvien.IdNhanVien.Value);
                        if (Ungvienkhaosat != null)
                        {
                            if (Ungvienkhaosat.Step == 1)
                            {
                                this.type.Value = "1";
                            }
                            if (Ungvienkhaosat.Step == 2)
                            {
                                this.type.Value = "2";
                            }
                            if (Ungvienkhaosat.Step == 3)
                            {
                                this.type.Value = "3";
                            }
                            VM_KhaoSat7Ngay ks7ngay = JsonConvert.DeserializeObject<VM_KhaoSat7Ngay>(Ungvienkhaosat.Ks7Ngay);
                            if (ks7ngay != null)
                            {
                                //7 Ngày
                                //Huong dan ngay dau
                                if (ks7ngay.Huongdanngaydau == 1)
                                    this.radHuongdanngaydau1.Checked = true;
                                if (ks7ngay.Huongdanngaydau == 2)
                                    this.radHuongdanngaydau2.Checked = true;
                                if (ks7ngay.Huongdanngaydau == 3)
                                    this.radHuongdanngaydau3.Checked = true;
                                if (ks7ngay.Huongdanngaydau == 4)
                                    this.radHuongdanngaydau4.Checked = true;
                                if (ks7ngay.Huongdanngaydau == 5)
                                    this.radHuongdanngaydau5.Checked = true;
                                //Tóm tắt nội quy làm việc
                                if (ks7ngay.Tomtatnoiquy == 1)
                                    this.radTomtatnoiquylamviec1.Checked = true;
                                if (ks7ngay.Tomtatnoiquy == 2)
                                    this.radTomtatnoiquylamviec2.Checked = true;
                                if (ks7ngay.Tomtatnoiquy == 3)
                                    this.radTomtatnoiquylamviec3.Checked = true;
                                if (ks7ngay.Tomtatnoiquy == 4)
                                    this.radTomtatnoiquylamviec4.Checked = true;
                                if (ks7ngay.Tomtatnoiquy == 5)
                                    this.radTomtatnoiquylamviec5.Checked = true;
                                //- Biên nhận bằng gốc
                                if (ks7ngay.Biennhan == 1)
                                    this.radBiennhanbanggoc1.Checked = true;
                                if (ks7ngay.Biennhan == 2)
                                    this.radBiennhanbanggoc2.Checked = true;
                                if (ks7ngay.Biennhan == 3)
                                    this.radBiennhanbanggoc3.Checked = true;
                                if (ks7ngay.Biennhan == 4)
                                    this.radBiennhanbanggoc4.Checked = true;
                                if (ks7ngay.Biennhan == 5)
                                    this.radBiennhanbanggoc5.Checked = true;
                                //- Tóm tắt chính sách phúc lợi
                                if (ks7ngay.Tomtatchinhsach == 1)
                                    this.radTomtatchinhsachphutloi1.Checked = true;
                                if (ks7ngay.Tomtatchinhsach == 2)
                                    this.radTomtatchinhsachphutloi2.Checked = true;
                                if (ks7ngay.Tomtatchinhsach == 3)
                                    this.radTomtatchinhsachphutloi3.Checked = true;
                                if (ks7ngay.Tomtatchinhsach == 4)
                                    this.radTomtatchinhsachphutloi4.Checked = true;
                                if (ks7ngay.Tomtatchinhsach == 5)
                                    this.radTomtatchinhsachphutloi5.Checked = true;
                                //- Cam kết thực hiện trách nhiệm lái xe	
                                if (ks7ngay.Camket == 1)
                                    this.radCamket1.Checked = true;
                                if (ks7ngay.Camket == 2)
                                    this.radCamket2.Checked = true;
                                if (ks7ngay.Camket == 3)
                                    this.radCamket3.Checked = true;
                                if (ks7ngay.Camket == 4)
                                    this.radCamket4.Checked = true;
                                if (ks7ngay.Camket == 5)
                                    this.radCamket5.Checked = true;
                                //	- Hướng dẫn ghi lịch trình hoạt động của lái xe
                                if (ks7ngay.Huongdan == 1)
                                    this.radHuongdan1.Checked = true;
                                if (ks7ngay.Huongdan == 2)
                                    this.radHuongdan2.Checked = true;
                                if (ks7ngay.Huongdan == 3)
                                    this.radHuongdan3.Checked = true;
                                if (ks7ngay.Huongdan == 4)
                                    this.radHuongdan4.Checked = true;
                                if (ks7ngay.Huongdan == 5)
                                    this.radHuongdan5.Checked = true;
                                //	-Những thông tin ngày đầu tiên nhận việc của phòng HCNS
                                if (ks7ngay.Nhungthongtinngaydau == 1)
                                    this.radNhungthongtinngaydau1.Checked = true;
                                if (ks7ngay.Nhungthongtinngaydau == 2)
                                    this.radNhungthongtinngaydau2.Checked = true;
                                if (ks7ngay.Nhungthongtinngaydau == 3)
                                    this.radNhungthongtinngaydau3.Checked = true;
                                if (ks7ngay.Nhungthongtinngaydau == 4)
                                    this.radNhungthongtinngaydau4.Checked = true;
                                if (ks7ngay.Nhungthongtinngaydau == 5)
                                    this.radNhungthongtinngaydau5.Checked = true;
                                //	-Những thông tin ngày đầu tiên nhận việc của phòng HCNS
                                if (ks7ngay.Noidunghuongdan == 1)
                                    this.radNoidunghuongdan1.Checked = true;
                                if (ks7ngay.Noidunghuongdan == 2)
                                    this.radNoidunghuongdan2.Checked = true;
                                if (ks7ngay.Noidunghuongdan == 3)
                                    this.radNoidunghuongdan3.Checked = true;
                                if (ks7ngay.Noidunghuongdan == 4)
                                    this.radNoidunghuongdan4.Checked = true;
                                if (ks7ngay.Noidunghuongdan == 5)
                                    this.radNoidunghuongdan5.Checked = true;
                                //	-File hướng dẫn hội nhập
                                if (ks7ngay.Filehuongdanhoinhap == 1)
                                    this.radFilehuongdanhoinhap1.Checked = true;
                                if (ks7ngay.Filehuongdanhoinhap == 2)
                                    this.radFilehuongdanhoinhap2.Checked = true;
                                if (ks7ngay.Filehuongdanhoinhap == 3)
                                    this.radFilehuongdanhoinhap3.Checked = true;
                                if (ks7ngay.Filehuongdanhoinhap == 4)
                                    this.radFilehuongdanhoinhap4.Checked = true;
                                if (ks7ngay.Filehuongdanhoinhap == 5)
                                    this.radFilehuongdanhoinhap5.Checked = true;
                                //	-File Nội quy công ty
                                if (ks7ngay.Filenoiquycongty == 1)
                                    this.radFilenoiquycongty1.Checked = true;
                                if (ks7ngay.Filenoiquycongty == 2)
                                    this.radFilenoiquycongty2.Checked = true;
                                if (ks7ngay.Filenoiquycongty == 3)
                                    this.radFilenoiquycongty3.Checked = true;
                                if (ks7ngay.Filenoiquycongty == 4)
                                    this.radFilenoiquycongty4.Checked = true;
                                if (ks7ngay.Filenoiquycongty == 5)
                                    this.radFilenoiquycongty5.Checked = true;
                                //	- File sơ đồ nhân sự công ty
                                if (ks7ngay.Filehosonhansu == 1)
                                    this.radFilehosonhansu1.Checked = true;
                                if (ks7ngay.Filehosonhansu == 2)
                                    this.radFilehosonhansu2.Checked = true;
                                if (ks7ngay.Filehosonhansu == 3)
                                    this.radFilehosonhansu3.Checked = true;
                                if (ks7ngay.Filehosonhansu == 4)
                                    this.radFilehosonhansu4.Checked = true;
                                if (ks7ngay.Filehosonhansu == 5)
                                    this.radFilehosonhansu5.Checked = true;
                                //	- Ngày đầu nhận việc
                                if (ks7ngay.Ngaydaunhanviec == 1)
                                    this.radNgaydaunhanviec1.Checked = true;
                                if (ks7ngay.Ngaydaunhanviec == 2)
                                    this.radNgaydaunhanviec2.Checked = true;
                                if (ks7ngay.Ngaydaunhanviec == 3)
                                    this.radNgaydaunhanviec3.Checked = true;
                                if (ks7ngay.Ngaydaunhanviec == 4)
                                    this.radNgaydaunhanviec4.Checked = true;
                                if (ks7ngay.Ngaydaunhanviec == 5)
                                    this.radNgaydaunhanviec5.Checked = true;
                                //	-Trao đổi với Phòng NS	
                                if (ks7ngay.Traodoivoiphongnhansu == 1)
                                    this.radTraodoivoiphongnhansu1.Checked = true;
                                if (ks7ngay.Traodoivoiphongnhansu == 2)
                                    this.radTraodoivoiphongnhansu2.Checked = true;
                                if (ks7ngay.Traodoivoiphongnhansu == 3)
                                    this.radTraodoivoiphongnhansu3.Checked = true;
                                if (ks7ngay.Traodoivoiphongnhansu == 4)
                                    this.radTraodoivoiphongnhansu4.Checked = true;
                                if (ks7ngay.Traodoivoiphongnhansu == 5)
                                    this.radTraodoivoiphongnhansu5.Checked = true;
                                //	-Tiếp xúc với nhân sự của bộ phận	
                                if (ks7ngay.Tiepxucvoinhansucuabophan == 1)
                                    this.radTiepxucvoinhansucuabophan1.Checked = true;
                                if (ks7ngay.Tiepxucvoinhansucuabophan == 2)
                                    this.radTiepxucvoinhansucuabophan2.Checked = true;
                                if (ks7ngay.Tiepxucvoinhansucuabophan == 3)
                                    this.radTiepxucvoinhansucuabophan3.Checked = true;
                                if (ks7ngay.Tiepxucvoinhansucuabophan == 4)
                                    this.radTiepxucvoinhansucuabophan4.Checked = true;
                                if (ks7ngay.Tiepxucvoinhansucuabophan == 5)
                                    this.radTiepxucvoinhansucuabophan5.Checked = true;
                                //	-Tiếp xúc với công việc tại bộ phận	
                                if (ks7ngay.Tiepxucvoicongviectaibophan == 1)
                                    this.radTiepxucvoicongviectaibophan1.Checked = true;
                                if (ks7ngay.Tiepxucvoicongviectaibophan == 2)
                                    this.radTiepxucvoicongviectaibophan2.Checked = true;
                                if (ks7ngay.Tiepxucvoicongviectaibophan == 3)
                                    this.radTiepxucvoicongviectaibophan3.Checked = true;
                                if (ks7ngay.Tiepxucvoicongviectaibophan == 4)
                                    this.radTiepxucvoicongviectaibophan4.Checked = true;
                                if (ks7ngay.Tiepxucvoicongviectaibophan == 5)
                                    this.radTiepxucvoicongviectaibophan5.Checked = true;
                                //- Đối với phòng HCNS	
                                this.doivoiphongnhansu.Value = ks7ngay.Doivoiphongnhansu;
                                this.doivoibophanlamviectructiep.Value = ks7ngay.Doivoibophanlamviectructiep;
                                this.doivoicongty.Value = ks7ngay.Doivoicongty;

                            }
                        }
                        else
                        {
                            //this.radHuongdanngaydau3.Checked = true;
                            //this.radTomtatnoiquylamviec3.Checked = true;
                            //this.radTomtatchinhsachphutloi3.Checked = true;
                            //this.radBiennhanbanggoc3.Checked = true;
                            //this.radCamket3.Checked = true;
                            //this.radHuongdan3.Checked = true;
                            //this.radNhungthongtinngaydau3.Checked = true;
                            //this.radNoidunghuongdan3.Checked = true;
                            //this.radFilehuongdanhoinhap3.Checked = true;
                            //this.radFilenoiquycongty3.Checked = true;
                            //this.radFilehosonhansu3.Checked = true;
                            //this.radNgaydaunhanviec3.Checked = true;
                            //this.radTraodoivoiphongnhansu3.Checked = true;
                            //this.radTiepxucvoinhansucuabophan3.Checked = true;
                            //this.radTiepxucvoicongviectaibophan3.Checked = true;
                        }
                        //14 ngày
                        if (Ungvienkhaosat != null)
                        {
                            if (!string.IsNullOrEmpty(Ungvienkhaosat.Ks14Ngay))
                            {
                                VM_KhaoSat14Ngay ks14ngay = JsonConvert.DeserializeObject<VM_KhaoSat14Ngay>(Ungvienkhaosat.Ks14Ngay);
                                if (ks14ngay != null)
                                {
                                    //Phần mềm quản lý nhân sự
                                    if (ks14ngay.Phanmemquanlynhansu == 1)
                                        this.radPhanmemquanlynhansu1.Checked = true;
                                    if (ks14ngay.Phanmemquanlynhansu == 2)
                                        this.radPhanmemquanlynhansu2.Checked = true;
                                    if (ks14ngay.Phanmemquanlynhansu == 3)
                                        this.radPhanmemquanlynhansu3.Checked = true;
                                    if (ks14ngay.Phanmemquanlynhansu == 4)
                                        this.radPhanmemquanlynhansu4.Checked = true;
                                    if (ks14ngay.Phanmemquanlynhansu == 5)
                                        this.radPhanmemquanlynhansu5.Checked = true;

                                    //- Sử dụng phần mềm đăng ký phép	
                                    if (ks14ngay.Sudungpmdkphep == 1)
                                        this.radSudungphanmemdangkyphep1.Checked = true;
                                    if (ks14ngay.Sudungpmdkphep == 2)
                                        this.radSudungphanmemdangkyphep2.Checked = true;
                                    if (ks14ngay.Sudungpmdkphep == 3)
                                        this.radSudungphanmemdangkyphep3.Checked = true;
                                    if (ks14ngay.Sudungpmdkphep == 4)
                                        this.radSudungphanmemdangkyphep4.Checked = true;
                                    if (ks14ngay.Sudungpmdkphep == 5)
                                        this.radSudungphanmemdangkyphep5.Checked = true;
                                    //-- Sử dụng phần mềm đăng ký tăng ca	
                                    if (ks14ngay.Sudungpmdktangca == 1)
                                        this.radSudungphanmemdangkytangca1.Checked = true;
                                    if (ks14ngay.Sudungpmdktangca == 2)
                                        this.radSudungphanmemdangkytangca2.Checked = true;
                                    if (ks14ngay.Sudungpmdktangca == 3)
                                        this.radSudungphanmemdangkytangca3.Checked = true;
                                    if (ks14ngay.Sudungpmdktangca == 4)
                                        this.radSudungphanmemdangkytangca4.Checked = true;
                                    if (ks14ngay.Sudungpmdktangca == 5)
                                        this.radSudungphanmemdangkytangca5.Checked = true;
                                    //- Sử dụng phần mềm đăng ký ghi công việc		
                                    if (ks14ngay.Sudungpmdkghicv == 1)
                                        this.radSudungphanmemdangkycongviec1.Checked = true;
                                    if (ks14ngay.Sudungpmdkghicv == 2)
                                        this.radSudungphanmemdangkycongviec2.Checked = true;
                                    if (ks14ngay.Sudungpmdkghicv == 3)
                                        this.radSudungphanmemdangkycongviec3.Checked = true;
                                    if (ks14ngay.Sudungpmdkghicv == 4)
                                        this.radSudungphanmemdangkycongviec4.Checked = true;
                                    if (ks14ngay.Sudungpmdkghicv == 5)
                                        this.radSudungphanmemdangkycongviec5.Checked = true;
                                    //- + Sử dụng phần mềm đăng ký ghi công việc (công tác)		
                                    if (ks14ngay.Sudungpmdkcvcongtac == 1)
                                        this.radSudungphanmemdangkycongviecct1.Checked = true;
                                    if (ks14ngay.Sudungpmdkcvcongtac == 2)
                                        this.radSudungphanmemdangkycongviecct2.Checked = true;
                                    if (ks14ngay.Sudungpmdkcvcongtac == 3)
                                        this.radSudungphanmemdangkycongviecct3.Checked = true;
                                    if (ks14ngay.Sudungpmdkcvcongtac == 4)
                                        this.radSudungphanmemdangkycongviecct4.Checked = true;
                                    if (ks14ngay.Sudungpmdkcvcongtac == 5)
                                        this.radSudungphanmemdangkycongviecct5.Checked = true;
                                    //-+ Sử dụng phần mềm đăng ký ghi công làm việc	
                                    if (ks14ngay.Sudungpmdkghiconglamviec == 1)
                                        this.radSudungphanmemdangkyghiconglamviec1.Checked = true;
                                    if (ks14ngay.Sudungpmdkghiconglamviec == 2)
                                        this.radSudungphanmemdangkyghiconglamviec2.Checked = true;
                                    if (ks14ngay.Sudungpmdkghiconglamviec == 3)
                                        this.radSudungphanmemdangkyghiconglamviec3.Checked = true;
                                    if (ks14ngay.Sudungpmdkghiconglamviec == 4)
                                        this.radSudungphanmemdangkyghiconglamviec4.Checked = true;
                                    if (ks14ngay.Sudungpmdkghiconglamviec == 5)
                                        this.radSudungphanmemdangkyghiconglamviec5.Checked = true;
                                    //-+ Sử dụng phần mềm đăng ký ghi công làm việc	
                                    if (ks14ngay.Sudungpmdkditrevesom == 1)
                                        this.radSudungphanmemdangkyvetre1.Checked = true;
                                    if (ks14ngay.Sudungpmdkditrevesom == 2)
                                        this.radSudungphanmemdangkyvetre2.Checked = true;
                                    if (ks14ngay.Sudungpmdkditrevesom == 3)
                                        this.radSudungphanmemdangkyvetre3.Checked = true;
                                    if (ks14ngay.Sudungpmdkditrevesom == 4)
                                        this.radSudungphanmemdangkyvetre4.Checked = true;
                                    if (ks14ngay.Sudungpmdkditrevesom == 5)
                                        this.radSudungphanmemdangkyvetre5.Checked = true;
                                    //-+ Sử dụng phần mềm đăng ký ghi công làm việc	
                                    if (ks14ngay.Sudungpmdkkiemtrachamcong == 1)
                                        this.radSudungphanmemkiemtrachamcong1.Checked = true;
                                    if (ks14ngay.Sudungpmdkkiemtrachamcong == 2)
                                        this.radSudungphanmemkiemtrachamcong2.Checked = true;
                                    if (ks14ngay.Sudungpmdkkiemtrachamcong == 3)
                                        this.radSudungphanmemkiemtrachamcong3.Checked = true;
                                    if (ks14ngay.Sudungpmdkkiemtrachamcong == 4)
                                        this.radSudungphanmemkiemtrachamcong4.Checked = true;
                                    if (ks14ngay.Sudungpmdkkiemtrachamcong == 5)
                                        this.radSudungphanmemkiemtrachamcong5.Checked = true;
                                    //-+ Sử dụng phần mềm đăng ký ghi công làm việc	
                                    if (ks14ngay.Phanmemdaotaocongty == 1)
                                        this.radPhanmemdaotaocongty1.Checked = true;
                                    if (ks14ngay.Phanmemdaotaocongty == 2)
                                        this.radPhanmemdaotaocongty2.Checked = true;
                                    if (ks14ngay.Phanmemdaotaocongty == 3)
                                        this.radPhanmemdaotaocongty3.Checked = true;
                                    if (ks14ngay.Phanmemdaotaocongty == 4)
                                        this.radPhanmemdaotaocongty4.Checked = true;
                                    if (ks14ngay.Phanmemdaotaocongty == 5)
                                        this.radPhanmemdaotaocongty5.Checked = true;
                                    //- Nội dung quy định chung	
                                    if (ks14ngay.Noidungquydinh == 1)
                                        this.radNoidungquydinhchung1.Checked = true;
                                    if (ks14ngay.Noidungquydinh == 2)
                                        this.radNoidungquydinhchung2.Checked = true;
                                    if (ks14ngay.Noidungquydinh == 3)
                                        this.radNoidungquydinhchung3.Checked = true;
                                    if (ks14ngay.Noidungquydinh == 4)
                                        this.radNoidungquydinhchung4.Checked = true;
                                    if (ks14ngay.Noidungquydinh == 5)
                                        this.radNoidungquydinhchung5.Checked = true;
                                    //- Nội dung chuyên môn công việc	
                                    if (ks14ngay.noidunghcuyenmoncongviec == 1)
                                        this.radNoidungchuyenmoncongviec1.Checked = true;
                                    if (ks14ngay.noidunghcuyenmoncongviec == 2)
                                        this.radNoidungchuyenmoncongviec2.Checked = true;
                                    if (ks14ngay.noidunghcuyenmoncongviec == 3)
                                        this.radNoidungchuyenmoncongviec3.Checked = true;
                                    if (ks14ngay.noidunghcuyenmoncongviec == 4)
                                        this.radNoidungchuyenmoncongviec4.Checked = true;
                                    if (ks14ngay.noidunghcuyenmoncongviec == 5)
                                        this.radNoidungchuyenmoncongviec5.Checked = true;
                                    this.doivoipmnhansu14.Value = ks14ngay.Gopyphanmemdnhansu;
                                    this.doivoiphanmemdaotao.Value = ks14ngay.Gopyphanmemdaotao;
                                }
                                
                            }
                            else
                            {
                                this.radPhanmemquanlynhansu3.Checked = true;
                                this.radSudungphanmemdangkyphep3.Checked = true;
                                this.radSudungphanmemdangkytangca3.Checked = true;
                                this.radSudungphanmemdangkycongviec3.Checked = true;
                                this.radSudungphanmemdangkycongviecct3.Checked = true;
                                this.radSudungphanmemdangkyghiconglamviec3.Checked = true;
                                this.radSudungphanmemdangkyvetre3.Checked = true;
                                this.radSudungphanmemkiemtrachamcong3.Checked = true;
                                this.radPhanmemdaotaocongty3.Checked = true;
                                this.radNoidungquydinhchung3.Checked = true;
                                this.radNoidungchuyenmoncongviec3.Checked = true;
                            }

                            if (!string.IsNullOrEmpty(Ungvienkhaosat.ks2Thang))
                            {
                                VM_Sau2Thang ks2thang = JsonConvert.DeserializeObject<VM_Sau2Thang>(Ungvienkhaosat.ks2Thang);
                                if (ks2thang != null)
                                {
                                    //-1 Tôi cảm thấy vô cùng hài lòng với công việc hiện tại
                                    if (ks2thang.Toicamthayvocunghailong == 1)
                                        this.radToicamthayvocunghailong1.Checked = true;
                                    if (ks2thang.Toicamthayvocunghailong == 2)
                                        this.radToicamthayvocunghailong2.Checked = true;
                                    if (ks2thang.Toicamthayvocunghailong == 3)
                                        this.radToicamthayvocunghailong3.Checked = true;
                                    if (ks2thang.Toicamthayvocunghailong == 4)
                                        this.radToicamthayvocunghailong4.Checked = true;
                                    if (ks2thang.Toicamthayvocunghailong == 5)
                                        this.radToicamthayvocunghailong5.Checked = true;
                                    //-Tôi cảm thấy công việc thú vị
                                    if (ks2thang.Toicamthaycongviecthuvi == 1)
                                        this.radToicamthaycongviecthuvi1.Checked = true;
                                    if (ks2thang.Toicamthaycongviecthuvi == 2)
                                        this.radToicamthaycongviecthuvi2.Checked = true;
                                    if (ks2thang.Toicamthaycongviecthuvi == 3)
                                        this.radToicamthaycongviecthuvi3.Checked = true;
                                    if (ks2thang.Toicamthaycongviecthuvi == 4)
                                        this.radToicamthaycongviecthuvi4.Checked = true;
                                    if (ks2thang.Toicamthaycongviecthuvi == 5)
                                        this.radToicamthaycongviecthuvi5.Checked = true;
                                    //-Tôi cảm thấy công việc thú vị
                                    if (ks2thang.Toibietchinhxaccongviec == 1)
                                        this.radToivietchinhxaccongviectoimuonlam1.Checked = true;
                                    if (ks2thang.Toibietchinhxaccongviec == 2)
                                        this.radToivietchinhxaccongviectoimuonlam2.Checked = true;
                                    if (ks2thang.Toibietchinhxaccongviec == 3)
                                        this.radToivietchinhxaccongviectoimuonlam3.Checked = true;
                                    if (ks2thang.Toibietchinhxaccongviec == 4)
                                        this.radToivietchinhxaccongviectoimuonlam4.Checked = true;
                                    if (ks2thang.Toibietchinhxaccongviec == 5)
                                        this.radToivietchinhxaccongviectoimuonlam5.Checked = true;
                                    //-Tôi sẵn sàng cố gắng hết sức để hoàn thành công việc	
                                    if (ks2thang.Toisansangcoganghetsuc == 1)
                                        this.radToisansangcoganghetsuc1.Checked = true;
                                    if (ks2thang.Toisansangcoganghetsuc == 2)
                                        this.radToisansangcoganghetsuc2.Checked = true;
                                    if (ks2thang.Toisansangcoganghetsuc == 3)
                                        this.radToisansangcoganghetsuc3.Checked = true;
                                    if (ks2thang.Toisansangcoganghetsuc == 4)
                                        this.radToisansangcoganghetsuc4.Checked = true;
                                    if (ks2thang.Toisansangcoganghetsuc == 5)
                                        this.radToisansangcoganghetsuc5.Checked = true;
                                    //-Công việc của tôi không quá thách thức
                                    if (ks2thang.Congviecucatoiquathachthuc == 1)
                                        this.radCongvieccuatoikhongquathachthuc1.Checked = true;
                                    if (ks2thang.Congviecucatoiquathachthuc == 2)
                                        this.radCongvieccuatoikhongquathachthuc2.Checked = true;
                                    if (ks2thang.Congviecucatoiquathachthuc == 3)
                                        this.radCongvieccuatoikhongquathachthuc3.Checked = true;
                                    if (ks2thang.Congviecucatoiquathachthuc == 4)
                                        this.radCongvieccuatoikhongquathachthuc4.Checked = true;
                                    if (ks2thang.Congviecucatoiquathachthuc == 5)
                                        this.radCongvieccuatoikhongquathachthuc5.Checked = true;
                                    //-Tôi được tự do quyết định cách làm việc	
                                    if (ks2thang.Toiduoctudoquyetdinhcachlamviec == 1)
                                        this.radToiduoctudoquyetdinh1.Checked = true;
                                    if (ks2thang.Toiduoctudoquyetdinhcachlamviec == 2)
                                        this.radToiduoctudoquyetdinh2.Checked = true;
                                    if (ks2thang.Toiduoctudoquyetdinhcachlamviec == 3)
                                        this.radToiduoctudoquyetdinh3.Checked = true;
                                    if (ks2thang.Toiduoctudoquyetdinhcachlamviec == 4)
                                        this.radToiduoctudoquyetdinh4.Checked = true;
                                    if (ks2thang.Toiduoctudoquyetdinhcachlamviec == 5)
                                        this.radToiduoctudoquyetdinh5.Checked = true;
                                    //-Tôi có nhiều cơ hội để học hỏi khi làm việc	
                                    if (ks2thang.Toiconhieucohoidehochoi == 1)
                                        this.radToiconhieucohoidehochoi1.Checked = true;
                                    if (ks2thang.Toiconhieucohoidehochoi == 2)
                                        this.radToiconhieucohoidehochoi2.Checked = true;
                                    if (ks2thang.Toiconhieucohoidehochoi == 3)
                                        this.radToiconhieucohoidehochoi3.Checked = true;
                                    if (ks2thang.Toiconhieucohoidehochoi == 4)
                                        this.radToiconhieucohoidehochoi4.Checked = true;
                                    if (ks2thang.Toiconhieucohoidehochoi == 5)
                                        this.radToiconhieucohoidehochoi5.Checked = true;
                                    //-Cơ sở hạ tầng/thiết bị/công cụ của công ty thật tuyệt vời	
                                    if (ks2thang.Cosohatangthietbicongcu == 1)
                                        this.radCosohatangthietbi1.Checked = true;
                                    if (ks2thang.Cosohatangthietbicongcu == 2)
                                        this.radCosohatangthietbi2.Checked = true;
                                    if (ks2thang.Cosohatangthietbicongcu == 3)
                                        this.radCosohatangthietbi3.Checked = true;
                                    if (ks2thang.Cosohatangthietbicongcu == 4)
                                        this.radCosohatangthietbi4.Checked = true;
                                    if (ks2thang.Cosohatangthietbicongcu == 5)
                                        this.radCosohatangthietbi5.Checked = true;
                                    //-Tôi không nhận được đủ sự hỗ trợ từ sếp trực tiếp của tôi	
                                    if (ks2thang.Toikhongnhanduocsuhotrotutructiep == 1)
                                        this.radToikhongnhanduocdusuhotro1.Checked = true;
                                    if (ks2thang.Toikhongnhanduocsuhotrotutructiep == 2)
                                        this.radToikhongnhanduocdusuhotro2.Checked = true;
                                    if (ks2thang.Toikhongnhanduocsuhotrotutructiep == 3)
                                        this.radToikhongnhanduocdusuhotro3.Checked = true;
                                    if (ks2thang.Toikhongnhanduocsuhotrotutructiep == 4)
                                        this.radToikhongnhanduocdusuhotro4.Checked = true;
                                    if (ks2thang.Toikhongnhanduocsuhotrotutructiep == 5)
                                        this.radToikhongnhanduocdusuhotro5.Checked = true;
                                    //-Tôi thích làm việc với sếp trực tiếp của tôi	
                                    if (ks2thang.Toithichlamviecvoiseptructiep == 1)
                                        this.radToithichlamviecvoiseptructiep1.Checked = true;
                                    if (ks2thang.Toithichlamviecvoiseptructiep == 2)
                                        this.radToithichlamviecvoiseptructiep1.Checked = true;
                                    if (ks2thang.Toithichlamviecvoiseptructiep == 3)
                                        this.radToithichlamviecvoiseptructiep1.Checked = true;
                                    if (ks2thang.Toithichlamviecvoiseptructiep == 4)
                                        this.radToithichlamviecvoiseptructiep1.Checked = true;
                                    if (ks2thang.Toithichlamviecvoiseptructiep == 5)
                                        this.radToithichlamviecvoiseptructiep1.Checked = true;
                                    //-Sự đóng góp của tôi được ghi nhận đầy đủ	
                                    if (ks2thang.Sudonggopcuatoiduocghinhan == 1)
                                        this.radSudonggopcuatoi1.Checked = true;
                                    if (ks2thang.Sudonggopcuatoiduocghinhan == 2)
                                        this.radSudonggopcuatoi2.Checked = true;
                                    if (ks2thang.Sudonggopcuatoiduocghinhan == 3)
                                        this.radSudonggopcuatoi3.Checked = true;
                                    if (ks2thang.Sudonggopcuatoiduocghinhan == 4)
                                        this.radSudonggopcuatoi4.Checked = true;
                                    if (ks2thang.Sudonggopcuatoiduocghinhan == 5)
                                        this.radSudonggopcuatoi5.Checked = true;
                                    //-Những kinh nghiệm tôi học được hiện tại cực kỳ hữu ích để phát triển sự nghiệp tương lai	
                                    if (ks2thang.Nhungkinhnghiemtoihocduochientaicuckihuuich == 1)
                                        this.radNhungkinhnghiemtoihocduoc1.Checked = true;
                                    if (ks2thang.Nhungkinhnghiemtoihocduochientaicuckihuuich == 2)
                                        this.radNhungkinhnghiemtoihocduoc2.Checked = true;
                                    if (ks2thang.Nhungkinhnghiemtoihocduochientaicuckihuuich == 3)
                                        this.radNhungkinhnghiemtoihocduoc3.Checked = true;
                                    if (ks2thang.Nhungkinhnghiemtoihocduochientaicuckihuuich == 4)
                                        this.radNhungkinhnghiemtoihocduoc4.Checked = true;
                                    if (ks2thang.Nhungkinhnghiemtoihocduochientaicuckihuuich == 5)
                                        this.radNhungkinhnghiemtoihocduoc5.Checked = true;
                                    //-Tôi thấy khó khăn để theo kịp các yêu cầu công việc	
                                    if (ks2thang.Thoithaykhokhandetheokip == 1)
                                        this.radToithaykhokhandetheokip1.Checked = true;
                                    if (ks2thang.Thoithaykhokhandetheokip == 2)
                                        this.radToithaykhokhandetheokip2.Checked = true;
                                    if (ks2thang.Thoithaykhokhandetheokip == 3)
                                        this.radToithaykhokhandetheokip3.Checked = true;
                                    if (ks2thang.Thoithaykhokhandetheokip == 4)
                                        this.radToithaykhokhandetheokip4.Checked = true;
                                    if (ks2thang.Thoithaykhokhandetheokip == 5)
                                        this.radToithaykhokhandetheokip5.Checked = true;
                                    //-Tôi không gặp bất kỳ khó khăn nào trong việc cân bằng công việc và cuộc sống cá nhân	
                                    if (ks2thang.toikhonggapbatcukhokhannao == 1)
                                        this.radToikhonggapbatkykhokhannao1.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 2)
                                        this.radToikhonggapbatkykhokhannao2.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 3)
                                        this.radToikhonggapbatkykhokhannao3.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 4)
                                        this.radToikhonggapbatkykhokhannao4.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 5)
                                        this.radToikhonggapbatkykhokhannao5.Checked = true;
                                    //14 Tôi không gặp bất kỳ khó khăn nào trong việc cân bằng công việc và cuộc sống cá nhân
                                    if (ks2thang.toikhonggapbatcukhokhannao == 1)
                                        this.radToikhonggapbatkykhokhannao1.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 2)
                                        this.radToikhonggapbatkykhokhannao2.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 3)
                                        this.radToikhonggapbatkykhokhannao3.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 4)
                                        this.radToikhonggapbatkykhokhannao4.Checked = true;
                                    if (ks2thang.toikhonggapbatcukhokhannao == 5)
                                        this.radToikhonggapbatkykhokhannao5.Checked = true;
                                    //15 Tôi hòa nhập dễ dàng với đồng nghiệpn
                                    if (ks2thang.Toihoanhapdedangvoidongnghiep == 1)
                                        this.radToihoanhapdedang1.Checked = true;
                                    if (ks2thang.Toihoanhapdedangvoidongnghiep == 2)
                                        this.radToihoanhapdedang2.Checked = true;
                                    if (ks2thang.Toihoanhapdedangvoidongnghiep == 3)
                                        this.radToihoanhapdedang3.Checked = true;
                                    if (ks2thang.Toihoanhapdedangvoidongnghiep == 4)
                                        this.radToihoanhapdedang4.Checked = true;
                                    if (ks2thang.Toihoanhapdedangvoidongnghiep == 5)
                                        this.radToihoanhapdedang5.Checked = true;
                                    //16 Tôi nghĩ đây là nơi tuyệt vời để làm việc
                                    if (ks2thang.Toinghidaylaoituyetvoidelamviec == 1)
                                        this.radToinghidaylanoituyetvoi1.Checked = true;
                                    if (ks2thang.Toinghidaylaoituyetvoidelamviec == 2)
                                        this.radToinghidaylanoituyetvoi2.Checked = true;
                                    if (ks2thang.Toinghidaylaoituyetvoidelamviec == 3)
                                        this.radToinghidaylanoituyetvoi3.Checked = true;
                                    if (ks2thang.Toinghidaylaoituyetvoidelamviec == 4)
                                        this.radToinghidaylanoituyetvoi4.Checked = true;
                                    if (ks2thang.Toinghidaylaoituyetvoidelamviec == 5)
                                        this.radToinghidaylanoituyetvoi5.Checked = true;
                                    //17 Tôi tin rằng mình sẽ có tương lai tươi sáng khi làm việc tại đây
                                    if (ks2thang.Toitinrangminhsecotuonglaituoisang == 1)
                                        this.radToitinrangminhsecotuonglai1.Checked = true;
                                    if (ks2thang.Toitinrangminhsecotuonglaituoisang == 2)
                                        this.radToitinrangminhsecotuonglai2.Checked = true;
                                    if (ks2thang.Toitinrangminhsecotuonglaituoisang == 3)
                                        this.radToitinrangminhsecotuonglai3.Checked = true;
                                    if (ks2thang.Toitinrangminhsecotuonglaituoisang == 4)
                                        this.radToitinrangminhsecotuonglai4.Checked = true;
                                    if (ks2thang.Toitinrangminhsecotuonglaituoisang == 5)
                                        this.radToitinrangminhsecotuonglai5.Checked = true;
                                    //18 Tôi sẽ tiếp tục làm việc tại đây	
                                    if (ks2thang.Toisetieptuclamviecoday == 1)
                                        this.radToisetieptuclamviectaiday1.Checked = true;
                                    if (ks2thang.Toisetieptuclamviecoday == 2)
                                        this.radToisetieptuclamviectaiday2.Checked = true;
                                    if (ks2thang.Toisetieptuclamviecoday == 3)
                                        this.radToisetieptuclamviectaiday3.Checked = true;
                                    if (ks2thang.Toisetieptuclamviecoday == 4)
                                        this.radToisetieptuclamviectaiday4.Checked = true;
                                    if (ks2thang.Toisetieptuclamviecoday == 5)
                                        this.radToisetieptuclamviectaiday5.Checked = true;
                                    //19 Tôi cảm thấy không hài lòng với giá trị của công ty và cách thức vận hành công việc kinh doanh	
                                    if (ks2thang.Toicamthaykhonghailongvoigiatricuacongty == 1)
                                        this.radToicamthaykhonghailongvoigiatricuacongty1.Checked = true;
                                    if (ks2thang.Toicamthaykhonghailongvoigiatricuacongty == 2)
                                        this.radToicamthaykhonghailongvoigiatricuacongty2.Checked = true;
                                    if (ks2thang.Toicamthaykhonghailongvoigiatricuacongty == 3)
                                        this.radToicamthaykhonghailongvoigiatricuacongty3.Checked = true;
                                    if (ks2thang.Toicamthaykhonghailongvoigiatricuacongty == 4)
                                        this.radToicamthaykhonghailongvoigiatricuacongty4.Checked = true;
                                    if (ks2thang.Toicamthaykhonghailongvoigiatricuacongty == 5)
                                        this.radToicamthaykhonghailongvoigiatricuacongty5.Checked = true;
                                    //20 Sản phẩm/Dịch vụ mà công ty cung cấp cực kỳ tuyệt vời
                                    if (ks2thang.Sanphamdichvumacongtycungcap == 1)
                                        this.radSanphamdichvu1.Checked = true;
                                    if (ks2thang.Sanphamdichvumacongtycungcap == 2)
                                        this.radSanphamdichvu2.Checked = true;
                                    if (ks2thang.Sanphamdichvumacongtycungcap == 3)
                                        this.radSanphamdichvu3.Checked = true;
                                    if (ks2thang.Sanphamdichvumacongtycungcap == 4)
                                        this.radSanphamdichvu4.Checked = true;
                                    if (ks2thang.Sanphamdichvumacongtycungcap == 5)
                                        this.radSanphamdichvu5.Checked = true;
                                }
                                
                            }
                            else
                            {
                                this.radToicamthayvocunghailong3.Checked = true;
                                this.radToicamthaycongviecthuvi3.Checked = true;
                                this.radToivietchinhxaccongviectoimuonlam3.Checked = true;
                                this.radToisansangcoganghetsuc3.Checked = true;
                                this.radCongvieccuatoikhongquathachthuc3.Checked = true;
                                this.radToiduoctudoquyetdinh3.Checked = true;
                                this.radToiconhieucohoidehochoi3.Checked = true;
                                this.radCosohatangthietbi3.Checked = true;
                                this.radToikhongnhanduocdusuhotro3.Checked = true;
                                this.radToithichlamviecvoiseptructiep3.Checked = true;
                                this.radSudonggopcuatoi3.Checked = true;
                                this.radNhungkinhnghiemtoihocduoc3.Checked = true;
                                this.radToithaykhokhandetheokip3.Checked = true;
                                this.radToikhonggapbatkykhokhannao3.Checked = true;
                                this.radToihoanhapdedang3.Checked = true;
                                this.radToinghidaylanoituyetvoi3.Checked = true;
                                this.radToitinrangminhsecotuonglai3.Checked = true;
                                this.radToisetieptuclamviectaiday3.Checked = true;
                                this.radToicamthaykhonghailongvoigiatricuacongty3.Checked = true;
                                this.radSanphamdichvu3.Checked = true;
                            }
                        }
                        else
                        {
                            
                            // 2 tháng
                           
                        }


                    }
                }

            }
        }
        public int UserMemberID
        {
            get
            {
                if (Session["g_UserMemberID"] != null)
                    return Convert.ToInt32(Session["g_UserMemberID"]);
                return 0;
            }
            set
            {
                Session["g_UserMemberID"] = value;
            }
        }
        protected void btnSaveBanner_Click(object sender, EventArgs e)
        {
            TUser tuser = blc_user.GetUser_ByIDAll(UserMemberID);
            Ungvienkhaosat uvks = blc_user.GetUngvienkhaosat(tuser.IdNhansu.Value);
            TUser user = blc_user.GetUser_ByIDAll(UserMemberID);
            var ungvien = blc_user.GetUngVienByIDNV(user.IdNhansu.Value);
            if (uvks == null)
            {
                VM_KhaoSat7Ngay VM_KhaoSat7Ngay = new VM_KhaoSat7Ngay();
                //radHuongdanngaydau
                var data = Page.Request.Form["ctl00$MainContent$radyk1"].ToString();
                var getdata = data.Replace("radHuongdanngaydau", "");
                VM_KhaoSat7Ngay.Huongdanngaydau = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk2"].ToString();
                getdata = data.Replace("radTomtatnoiquylamviec", "");
                VM_KhaoSat7Ngay.Tomtatnoiquy = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk3"].ToString();
                getdata = data.Replace("radTomtatchinhsachphutloi", "");
                VM_KhaoSat7Ngay.Tomtatchinhsach = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk4"].ToString();
                getdata = data.Replace("radBiennhanbanggoc", "");
                VM_KhaoSat7Ngay.Biennhan = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk5"].ToString();
                getdata = data.Replace("radCamket", "");
                VM_KhaoSat7Ngay.Camket = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk6"].ToString();
                getdata = data.Replace("radHuongdan", "");
                VM_KhaoSat7Ngay.Huongdan = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk7"].ToString();
                getdata = data.Replace("radNhungthongtinngaydau", "");
                VM_KhaoSat7Ngay.Nhungthongtinngaydau = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk8"].ToString();
                getdata = data.Replace("radNoidunghuongdan", "");
                VM_KhaoSat7Ngay.Noidunghuongdan = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk9"].ToString();
                getdata = data.Replace("radFilehuongdanhoinhap", "");
                VM_KhaoSat7Ngay.Filehuongdanhoinhap = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk10"].ToString();
                getdata = data.Replace("radFilenoiquycongty", "");
                VM_KhaoSat7Ngay.Filenoiquycongty = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk11"].ToString();
                getdata = data.Replace("radFilehosonhansu", "");
                VM_KhaoSat7Ngay.Filehosonhansu = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk12"].ToString();
                getdata = data.Replace("radNgaydaunhanviec", "");
                VM_KhaoSat7Ngay.Ngaydaunhanviec = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk13"].ToString();
                getdata = data.Replace("radTraodoivoiphongnhansu", "");
                VM_KhaoSat7Ngay.Traodoivoiphongnhansu = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk14"].ToString();
                getdata = data.Replace("radTiepxucvoinhansucuabophan", "");
                VM_KhaoSat7Ngay.Tiepxucvoinhansucuabophan = int.Parse(getdata);
                //
                data = Page.Request.Form["ctl00$MainContent$radyk15"].ToString();
                getdata = data.Replace("radTiepxucvoicongviectaibophan", "");
                VM_KhaoSat7Ngay.Tiepxucvoicongviectaibophan = int.Parse(getdata);
                //
                VM_KhaoSat7Ngay.Doivoiphongnhansu = Page.Request.Form["ctl00$MainContent$doivoiphongnhansu"].ToString();
                VM_KhaoSat7Ngay.Doivoibophanlamviectructiep = Page.Request.Form["ctl00$MainContent$doivoibophanlamviectructiep"].ToString();
                VM_KhaoSat7Ngay.Doivoicongty = Page.Request.Form["ctl00$MainContent$doivoicongty"].ToString();

              
                blc_user.CapnhatKhaosat7Ngay(ungvien.IdNhanVien.Value, VM_KhaoSat7Ngay);
                //Ghi log Message
                blc_user.InsertMessage(user.UserName.ToUpper()+ " Đã đánh giá khảo sát 07 ngày làm việc");
            }
            else
            {
                if (uvks.Step == 2)
                {
                    //14 ngày
                    VM_KhaoSat14Ngay VM_KhaoSat14Ngay = new VM_KhaoSat14Ngay();
                    //
                    var data = Page.Request.Form["ctl00$MainContent$radt2yk1"].ToString();
                    var getdata = data.Replace("radPhanmemquanlynhansu", "");
                    VM_KhaoSat14Ngay.Phanmemquanlynhansu = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk2"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkyphep", "");
                    VM_KhaoSat14Ngay.Sudungpmdkphep = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk3"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkytangca", "");
                    VM_KhaoSat14Ngay.Sudungpmdktangca = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk4"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkycongviec", "");
                    VM_KhaoSat14Ngay.Sudungpmdkghicv = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk5"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkycongviecct", "");
                    VM_KhaoSat14Ngay.Sudungpmdkcvcongtac = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk6"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkyghiconglamviec", "");
                    VM_KhaoSat14Ngay.Sudungpmdkghiconglamviec = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk7"].ToString();
                    getdata = data.Replace("radSudungphanmemdangkyvetre", "");
                    VM_KhaoSat14Ngay.Sudungpmdkditrevesom = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk8"].ToString();
                    getdata = data.Replace("radSudungphanmemkiemtrachamcong", "");
                    VM_KhaoSat14Ngay.Sudungpmdkkiemtrachamcong = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk9"].ToString();
                    getdata = data.Replace("radPhanmemdaotaocongty", "");
                    VM_KhaoSat14Ngay.Phanmemdaotaocongty = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk10"].ToString();
                    getdata = data.Replace("radNoidungquydinhchung", "");
                    VM_KhaoSat14Ngay.Noidungquydinh = int.Parse(getdata);
                    //
                    data = Page.Request.Form["ctl00$MainContent$radt2yk11"].ToString();
                    getdata = data.Replace("radNoidungchuyenmoncongviec", "");
                    VM_KhaoSat14Ngay.noidunghcuyenmoncongviec = int.Parse(getdata);
                    //
                    VM_KhaoSat14Ngay.Gopyphanmemdnhansu = Page.Request.Form["ctl00$MainContent$doivoipmnhansu14"].ToString();
                    VM_KhaoSat14Ngay.Gopyphanmemdaotao = Page.Request.Form["ctl00$MainContent$doivoiphanmemdaotao"].ToString();
                    blc_user.CapnhatKhaosat14Ngay(ungvien.IdNhanVien.Value, VM_KhaoSat14Ngay);

                }
                if (uvks.Step == 3)
                {
                    VM_Sau2Thang VM_Sau2Thang = new VM_Sau2Thang();
                   var data = Page.Request.Form["ctl00$MainContent$radt3yk1"].ToString();
                   var  getdata = data.Replace("radToicamthayvocunghailong", "");
                    VM_Sau2Thang.Toicamthayvocunghailong = int.Parse(getdata);
                    //2
                    data = Page.Request.Form["ctl00$MainContent$radt3yk2"].ToString();
                    getdata = data.Replace("radToicamthaycongviecthuvi", "");
                    VM_Sau2Thang.Toicamthaycongviecthuvi = int.Parse(getdata);
                    //3
                    data = Page.Request.Form["ctl00$MainContent$radt3yk3"].ToString();
                    getdata = data.Replace("radToivietchinhxaccongviectoimuonlam", "");
                    VM_Sau2Thang.Toibietchinhxaccongviec = int.Parse(getdata);
                    //4
                    data = Page.Request.Form["ctl00$MainContent$radt3yk4"].ToString();
                    getdata = data.Replace("radToisansangcoganghetsuc", "");
                    VM_Sau2Thang.Toisansangcoganghetsuc = int.Parse(getdata);
                    //5
                    data = Page.Request.Form["ctl00$MainContent$radt3yk5"].ToString();
                    getdata = data.Replace("radCongvieccuatoikhongquathachthuc", "");
                    VM_Sau2Thang.Congviecucatoiquathachthuc = int.Parse(getdata);
                    //6
                    data = Page.Request.Form["ctl00$MainContent$radt3yk6"].ToString();
                    getdata = data.Replace("radToiduoctudoquyetdinh", "");
                    VM_Sau2Thang.Toiduoctudoquyetdinhcachlamviec = int.Parse(getdata);
                    //7
                    data = Page.Request.Form["ctl00$MainContent$radt3yk7"].ToString();
                    getdata = data.Replace("radToiconhieucohoidehochoi", "");
                    VM_Sau2Thang.Toiconhieucohoidehochoi = int.Parse(getdata);
                    //8
                    data = Page.Request.Form["ctl00$MainContent$radt3yk8"].ToString();
                    getdata = data.Replace("radCosohatangthietbi", "");
                    VM_Sau2Thang.Cosohatangthietbicongcu = int.Parse(getdata);
                    //9
                    data = Page.Request.Form["ctl00$MainContent$radt3yk9"].ToString();
                    getdata = data.Replace("radToikhongnhanduocdusuhotro", "");
                    VM_Sau2Thang.Toikhongnhanduocsuhotrotutructiep = int.Parse(getdata);
                    //10
                    data = Page.Request.Form["ctl00$MainContent$radt3yk10"].ToString();
                    getdata = data.Replace("radToithichlamviecvoiseptructiep", "");
                    VM_Sau2Thang.Toithichlamviecvoiseptructiep = int.Parse(getdata);
                    //11
                    data = Page.Request.Form["ctl00$MainContent$radt3yk11"].ToString();
                    getdata = data.Replace("radSudonggopcuatoi", "");
                    VM_Sau2Thang.Sudonggopcuatoiduocghinhan = int.Parse(getdata);
                    //12
                    data = Page.Request.Form["ctl00$MainContent$radt3yk12"].ToString();
                    getdata = data.Replace("radNhungkinhnghiemtoihocduoc", "");
                    VM_Sau2Thang.Nhungkinhnghiemtoihocduochientaicuckihuuich = int.Parse(getdata);
                    //13
                    data = Page.Request.Form["ctl00$MainContent$radt3yk13"].ToString();
                    getdata = data.Replace("radToithaykhokhandetheokip", "");
                    VM_Sau2Thang.Thoithaykhokhandetheokip = int.Parse(getdata);
                    //14
                    data = Page.Request.Form["ctl00$MainContent$radt3yk14"].ToString();
                    getdata = data.Replace("radToikhonggapbatkykhokhannao", "");
                    VM_Sau2Thang.toikhonggapbatcukhokhannao = int.Parse(getdata);
                    //15
                    data = Page.Request.Form["ctl00$MainContent$radt3yk15"].ToString();
                    getdata = data.Replace("radToihoanhapdedang", "");
                    VM_Sau2Thang.Toihoanhapdedangvoidongnghiep = int.Parse(getdata);
                    //16
                    data = Page.Request.Form["ctl00$MainContent$radt3yk16"].ToString();
                    getdata = data.Replace("radToinghidaylanoituyetvoi", "");
                    VM_Sau2Thang.Toinghidaylaoituyetvoidelamviec = int.Parse(getdata);
                    //17
                    data = Page.Request.Form["ctl00$MainContent$radt3yk17"].ToString();
                    getdata = data.Replace("radToitinrangminhsecotuonglai", "");
                    VM_Sau2Thang.Toitinrangminhsecotuonglaituoisang = int.Parse(getdata);
                    //18
                    data = Page.Request.Form["ctl00$MainContent$radt3yk18"].ToString();
                    getdata = data.Replace("radToisetieptuclamviectaiday", "");
                    VM_Sau2Thang.Toisetieptuclamviecoday = int.Parse(getdata);
                    //19
                    data = Page.Request.Form["ctl00$MainContent$radt3yk19"].ToString();
                    getdata = data.Replace("radToicamthaykhonghailongvoigiatricuacongty", "");
                    VM_Sau2Thang.Toicamthaykhonghailongvoigiatricuacongty = int.Parse(getdata);
                    //20
                    data = Page.Request.Form["ctl00$MainContent$radt3yk20"].ToString();
                    getdata = data.Replace("radSanphamdichvu", "");
                    VM_Sau2Thang.Sanphamdichvumacongtycungcap = int.Parse(getdata);
                    blc_user.CapnhatKhaosat2Thang(ungvien.IdNhanVien.Value, VM_Sau2Thang);
                }
                    //Sau 2 tháng
                   
            }
        }
    }
}