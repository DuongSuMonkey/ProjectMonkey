using BookCurlPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitOptionHandler :IMenuOptionHandler
{
    private BookPro bookPro;
    private DetailMenu detailMenu;
    public ExitOptionHandler(BookPro bookPro,DetailMenu detailMenu)
    {
        this.bookPro = bookPro;
        this.detailMenu = detailMenu;
    }
    public void HandleOption()
    {
        bookPro.gameObject.SetActive(false);
        detailMenu.gameObject.SetActive(true);
    }
}
