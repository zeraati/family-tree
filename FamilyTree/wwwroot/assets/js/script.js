const PersonFamilyUpdate = (data) => {
    const fullName = data.name.split(' ');
    let spouseIds = [];
    data.pids.forEach((value) => { if (!isNaN(value)) spouseIds.push(Number(value)); })

    let childrenIds = [];
    family.nodes[data.id].ftChildrenIds.forEach((value) => {
        if (!isNaN(value)) childrenIds.push(value);
    });

    const body = {
        tempeId: isNaN(data.id) ? data.id:null,
        personId: !isNaN(data.id) ? data.id : null,
        firsrtName: fullName[0],
        lastName: fullName.length > 1 ? fullName[1]:"",
        genderId: data.gender == 'male' ? 1 : data.gender == 'female' ? 2 : null,
        fatherId: !isNaN(data.fid) ? data.fid : null,
        motherId: !isNaN(data.mid) ? data.mid : null,
        spouseIds: spouseIds,
        childrenIds: childrenIds
    };

    let url = '/api/PersonWithFamily';
    let method = 'POST';

    if (body.personId > 0) {
        url = '/api/PersonWithFamily/' + body.personId;
        method = 'PUT';
    }

    CallAPI(url, method, body)
}

const PersonFamilyDelete = (id) => {
    let url = '/api/PersonWithFamily/' + id;
    let method = 'DELETE';
    CallAPI(url, method)
}

const CallAPI = async (url, method = 'POST', body = {}) => {debugger
    const response = await fetch(url, {
        method: method,
        body: method == 'POST' || method == 'PUT' ? JSON.stringify(body) : null,
        headers: { 'Content-Type': 'application/json' }
    });

    const result = await response.json();
    Toast(result.message, result.status);
    if (response.ok == false) {
        console.log(method, url, JSON.stringify(body));
        console.log(result);
    }
    //if (response.ok && result.status) {
    //    Toast(result.message);
    //} else {
    //    Toast()
    //    console.log(result);
    //}
}

const Toast = (message, statusId) => {
    let status = null;
    switch (statusId) {
        case 1: status = 'success'; break;
        case 2: status = 'danger'; break;
        case 400: status = 'danger'; break;
    }

    SnackBar({
        message: message,
        //width: "1000px",
        status: status,// null=1 || info=1 || success=3 || warning=4 || danger=5 ,
        //icon: "plus" // danger || info || plus || ...
        //timeout: 5000 // ms
        dismissible: false,
        //speed: 500
        position: "bl" // tl || tc || tr || bl || bc || br
        //fixed: true
    });

    if (statusId == 1) { setTimeout(() => { location.reload(); }, 1000); }
}