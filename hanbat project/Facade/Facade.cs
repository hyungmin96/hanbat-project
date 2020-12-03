using System.Net;

namespace hanbat_project.Facade
{
    public class FacadeClass
    {

        private getClasses getclass;
        private getIdInfo getIdInfo;
        private getDate GetDate;

        public FacadeClass(Main main)
        {
            getclass = new getClasses(main);
            getIdInfo = new getIdInfo(main);
            GetDate = new getDate(main);
        }

        public void displayInfo()
        {
            getclass.getClassList();
            getIdInfo.getInfo();
            GetDate.getDateMethod();
        }

    }

}
