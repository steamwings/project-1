
function checkSubmit_New(){
    if(!hasVal("Name")){
        hide("divSubmit");
        return;
    };
    var type = document.getElementById("TypeId").value;
    switch (type) {
        case "1": // Checking
            if(hasVal("InterestId_Checking"))
                break;
        case "2": // Loan
            if(hasVal("LoanAmount") && hasVal("InterestId_Loan"))
                break;
        case "3": // Investment
            if(hasVal("InterestId_Investment"))
                break;
        default:
            hide("divSubmit");
            return;
    }
    show("divSubmit");
}

function checkSubmit_Tx() {
    if (!hasVal("Amount") || !hasVal("selectInto") || !hasVal("selectOutOf") ) {
        hide("divSubmit");
        return;
    };
    show("divSubmit");
}


function checkSubmit_WD() {
    if (!hasVal("Amount") || !hasVal("selectAccount")) {
        hide("divSubmit");
        return;
    };
    show("divSubmit");
}

function updateFromType_New() {
    // document.getElementById("InterestId_Checking").value = "";
    // document.getElementById("InterestId_Investment").value = "";
    // document.getElementById("InterestId_Loan").value = "";
    var type = document.getElementById("TypeId").value;
    switch (type) {
        case "2": // Loan
            show("divLoan");
            hide("divTerm");
            hide("divChecking");
            break;
        case "3": // Investment
            hide("divLoan");
            show("divTerm");
            hide("divChecking");
            break;
        case "1": // Checking
            hide("divLoan");
            hide("divTerm");
            show("divChecking");
            break;
        default:
            hide("divLoan");
            hide("divTerm");
            hide("divChecking");
    }
}

function disableOption_Tx(selectModified, selectToModify){
    var val = selectedVal(selectModified);
    if(val == document.getElementById(selectToModify).value)
        document.getElementById(selectToModify).value = "";
}

function disableBtn_WD(btnModified, btnToModify) {
    show("divWDForm");
    document.getElementById(btnModified).disabled = true;
    document.getElementById(btnToModify).disabled = false;
    document.getElementById("divSubmit").value = btnModified;
}

// -------------- Helper functions --------------

function selectedVal(selectName){
    var op = document.getElementById(selectName).getElementsByTagName("option");
    for (var i = 0; i < op.length; i++) {
        if(op[i].selected)
            return op[i].value;
    }
}

function hello() { alert("Hello!"); }

function show(id) {
    // alert("Hello!");
    document.getElementById(id).style.display = "block";
}
function hide(id) {
    document.getElementById(id).style.display = "none";
}
function hasVal(id) {
    var v = document.getElementById(id).value;
    return !(v === undefined || v === "");
}
