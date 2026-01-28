namespace AndrewMercuryTestHub.Models
{
    public class myoffice
    {
        public required string acpd_sid { get; set; }
        public required string acpd_cname { get; set; }// nvarchar 60
        public required string acpd_ename { get; set; }// nvarchar 40
        public required string acpd_sname { get; set; }  // nvarchar 40
        public required string acpd_email { get; set; } // nvarchar 60
        public int acpd_status { get; set; }// 0..255
        public char acpd_stop { get; set; } // 0/1
        public required string acpd_stopMemo { get; set; } // 600
        public required string acpd_LoginID { get; set; } // nv30  登入帳號
        public required string acpd_LoginPW { get; set; } // nv60 登入密碼
        public required string acpd_memo { get; set; } // nv120 備註
        public DateTime acpd_nowdatetime { get; set; } // 新增日期
        public required string appd_nowid { get; set; } // nv20 新增人員代碼
        public DateTime acpd_upddatetitme { get; set; } // 修改日期
        public required string acpd_updid { get; set; } // nv20 修改人員代碼

    }// public class myoffice
}
