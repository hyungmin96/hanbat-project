using System.Net;

namespace hanbat_project.Facade
{
    public class FacadeClass
    {

        private getClasses getclass;
        private getIdInfo getIdInfo;
        private getDate GetDate;
        private getAssignment getAssignment;

        public FacadeClass()
        {
            getclass = new getClasses();
            getIdInfo = new getIdInfo();
            GetDate = new getDate();
            getAssignment = new getAssignment();
        }

        public void displayInfo()
        {
            getIdInfo.getInfo();
            GetDate.getDateMethod();
        }

        public void displayItems()
        {
            getclass.getClassList();
            getAssignment.getAssignmentList();
        }

    }

}
