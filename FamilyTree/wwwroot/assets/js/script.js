const PersonFamilyUpdate = (data) => {
    const fullName = data.name.split(' ');
    let spouseIds = [];
    data.pids.forEach((value) => { if (!isNaN(value)) spouseIds.push(Number(value)); })

    let childrenIds = [];
    family.nodes[data.id].ftChildrenIds.forEach((value) => {
        if (!isNaN(value)) childrenIds.push(value);
    });

    const body = {
        tempeId: isNaN(data.id) ? data.id : null,
        personId: !isNaN(data.id) ? data.id : null,
        firsrtName: fullName[0],
        lastName: fullName.length > 1 ? fullName[1] : "",
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
    CallAPI(url, method);
}

const PersonFamilyUploadPhoto = (id, file) => {
    const data = new FormData()
    data.append('file', file)

    let url = '/api/PersonWithFamily/UploadPhoto/' + id;
    let method = 'POST';
    CallAPI(url, method, data);
}

const CallAPI = async (url, method = 'POST', body = {}) => {

    var options = { method: method };

    if (url.includes('UploadPhoto')) options.body = body;
    else if (method == 'POST' || method == 'PUT') {
        options.body = JSON.stringify(body);
        options.headers={ 'Content-Type':'application/json' }
    }

    const response = await fetch(url, options);
    const result = await response.json();

    if (response.ok == true) {
        Toast(result.message, result.status);
        setTimeout(() => { location.reload(); }, 1000);
    }
    else {
        Toast(result.message, result.status);
        console.log(method, url, JSON.stringify(body));
        console.log(result);
    }
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
        status: status,// null=1 || info=1 || success=3 || warning=4 || danger=5 ,
        dismissible: false,
        position: "bl" // tl || tc || tr || bl || bc || br
        //fixed: true,
        //width: "1000px",
        //speed: 500,
        //timeout: 5000 // ms
        //icon: "plus" // danger || info || plus || ...
    });
}