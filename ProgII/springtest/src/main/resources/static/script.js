const FORM_MODE_NONE=0;
const FORM_MODE_EDIT=1;
const FORM_MODE_ADD=2;

var FormMode = FORM_MODE_NONE;
var CurrentObject = null;

var Books = null;

/* UI functions */
function AddBook() 
{
	$("#editFormButton").val("Add");
	ClearFormData();
	FormMode = FORM_MODE_ADD;
	$(".editForm").css("display","block");
}
function EditBook(id) 
{
	$("#editFormButton").val("Modify");
	FormMode = FORM_MODE_EDIT;
	CurrentObject = id;
	DeployFormData();
	$(".editForm").css("display","block");
}
function DeleteBook(id) 
{
	CurrentObject = id; 
	RemoveBook(); 
}

function SubmitEdit() 
{
	switch(FormMode)
	{
		case FORM_MODE_NONE: break;
		case FORM_MODE_EDIT: ModifyBook(); break;
		case FORM_MODE_ADD: CreateBook(); break;
	}
}

function CancelEdit()
{
	$(".editForm").css("display","none");
	FormMode = FORM_MODE_NONE;
	CurrentObject = null;
}
/* UI functions - end */

/* Data functions */
function ClearFormData()
{
	const form_fields = [ "Title", "Publisher", "ISBN", "Author", "Edition", "YearPublished" ];
	
	for(i = 0; i<form_fields.length; i++)
	{
		var fieldId="#editForm"+form_fields[i];
		var input = $(fieldId);
		input.val("");
	}
}

function DeployFormData()
{
	const form_fields = [ "Title", "Publisher", "ISBN", "Author", "Edition", "YearPublished" ];
	const data_fields = [ "title", "publisher", "isbn", "author", "edition", "yearPublished" ];
	
	var book = Books.find((x) => x.id == CurrentObject);
	
	if(book == null) return false;
	
	for(i = 0; i<form_fields.length; i++)
	{
		var fieldId="#editForm"+form_fields[i];
		var input = $(fieldId);
		input.val(book[data_fields[i]]);
	}
}

function ExtractFormData()
{
	const form_fields = [ "Title", "Publisher", "ISBN", "Author", "Edition", "YearPublished" ];
	
	var book = {ID:CurrentObject};
	
	for(i = 0; i<form_fields.length; i++)
	{
		var fieldId="#editForm"+form_fields[i];
		var input = $(fieldId);
		book[form_fields[i]] = input.val();
	}
	
	return book;	
}

function CreateBook()
{
	var url="/book/create";
	
	var book = ExtractFormData();
	book.ID=0;
	
	$.post(url, book, function(data,status){ 
		GetBooks();
		CancelEdit();
	 });
}

function RemoveBook()
{
	if(CurrentObject==null) return;
	
	var url="/book/delete";
	
	$.get(url, {ID:CurrentObject}, function(data,status)
	{
		GetBooks();
		CancelEdit();
	});
}

function ModifyBook()
{
	var url="/book/update";
	
	var book = ExtractFormData();
	
	$.post(url, book, function(data,status)
	{
		GetBooks();
		CancelEdit();
	});
}

function GetBooks()
{
	var url="/book/listjson";
	
	$.getJSON(url, function(data, status)
	{
		var table = $("#bookTable");
		table.empty();
		
		Books = data;
		
		for(var i=0; i<data.length; i++)
		{
			var tr = document.createElement("tr");
			
			tr.setAttribute("title", data[i].id);
			
			var fields = [ "id", "title", "publisher", "isbn", "author", "edition", "yearPublished" ];
			// <tr>
			//		<td>...</td>
			for(var j=1; j<fields.length; j++)
			{
				var td = document.createElement("td");
				td.innerText = data[i][fields[j]];
				tr.append(td);
			}
			
			var tdedit = $(document.createElement("td"));
			var btnedit = $(document.createElement("button"));
			var btndelete = $(document.createElement("button"));
			
			btnedit.text("Edit");
			btndelete.text("Delete");
			
			btnedit.addClass("edit-button");
			btndelete.addClass("delete-button");
			
			btnedit.data("entryid",data[i]["id"]);
			btndelete.data("entryid",data[i]["id"]);
			
			// Ошибка здесь
			//btnedit.onclick = () => EditBook(data[i]["id"]);
			//btndelete.onclick = () => DeleteBook(data[i]["id"]);
			
			tdedit.append(btnedit);
			tdedit.append(btndelete);
			
			tr.append(tdedit.get(0));
			
			table.append(tr);
		}
		
		$(".edit-button").on("click",function(event){
			var el = $(event.target);
			EditBook(el.data("entryid"));
		})
		
		$(".delete-button").on("click",function(event){
			var el = $(event.target);
			DeleteBook(el.data("entryid"));
		})
	});
}

/* Data functions */

$(document).ready(function()
{
	GetBooks();
})