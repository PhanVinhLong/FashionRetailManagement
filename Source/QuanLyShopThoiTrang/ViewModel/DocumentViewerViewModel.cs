using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyShopThoiTrang.Model;
using DevExpress.Xpf.Printing;

namespace QuanLyShopThoiTrang.ViewModel
{
    public class DocumentViewerViewModel
    {
        public DocumentViewerViewModel()
        {
            
        }

        public DocumentViewerViewModel(HoaDon hd, DocumentPreviewControl dpControl)
        {
            HoaDonReport reportWd = new HoaDonReport(hd);
            dpControl.DocumentSource = reportWd;
            reportWd.CreateDocument();
        }
    }
}
