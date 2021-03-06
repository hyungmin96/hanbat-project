﻿using hanbat_project.Class;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using WindowsFormsApp2.Class;

namespace hanbat_project.Strategy
{
    public class getContent
    {

        public void get_Content()
        {

            String _classNum = MainForm.main.customListView2.FocusedItem.SubItems[5].Text;

            String postData = "boardInfoDTO.boardInfoId=" + getNotice.BoardId + "" +
                "&boardInfoDTO.boardInfoGubun=notice&boardContentsDTO.boardContentsId=" + Board.board.customListView2.FocusedItem.SubItems[4].Text + "" +
                "&courseDTO.courseId=" + _classNum +
                "&boardInfoDTO.boardClass=notice&page=&cmd=viewBoardContents&gubun=V&type=&reComment=&searchTxt=";

            Uri _uri = new Uri("http://cyber.hanbat.ac.kr/MCourse.do");

            setPost setpost = new setPost();
            setpost.method(new setHttpProtocol(_uri, postData));

            String _subject = Regex.Split(Regex.Split(setpost._html, "목록</a>")[1], "<ul class=\"comm_file\">")[0];

            _subject = Option.StripHTML(Regex.Replace(Regex.Replace(_subject, "<br>", "\n").Trim(), "&nbsp;", String.Empty));

            Board.board.customRichTextBox1.setValue = _subject;

        }

    }

}
