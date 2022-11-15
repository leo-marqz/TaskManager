import { Task } from "/js/Task.js";

const url = "/api/tasks";
const containerTasks = document.getElementById("containter-my-tasks");
const formAddTask = document.getElementById("form-add-task");

formAddTask.addEventListener("submit", function (e) {
    e.preventDefault();
    console.info(this.action);
    let title = e.target[0].value;
    fetch(this.action, {
        method: this.method,
        body: JSON.stringify(title),
        headers: {
            "Content-Type": "application/json"
        }
    })
        .then(response => {
            if (response.ok) {
                e.target[0].value = "";
                showAlert("success", "container-alert", "Tarea guardada exitosamente :)");
            } else {
                showAlert("danger", "container-alert", "Algo salio mal, tarea no guardada :(");
            }
        })
        .catch(error => {
            console.log(error);
            showAlert("danger", "container-alert", "Algo salio mal, tarea no guardada :(");
        });
});



function getTask() {
    spinner(true);
    let response = [];
    fetch(urlGet, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        }
    }).then((response) => {
        console.log(response);
        return response.json()
    })
        .then((data) => {
            spinner(false);
            console.log(data);
        })
        .catch((err) => {
            console.log(err);
        });
}
getTask();



//======================================================================

function showAlert(type = "danger", idHtmlElement = null, message = "") {
    let alert = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">${message} !!<button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button></div>`;
    if (idHtmlElement == null) return;
    $(`#${idHtmlElement}`).html(alert);
}

function spinner(active = false) {
    const spinner = `<div id="spinner" class="spinner-border text-danger" role="status">
                        <span class="visually-hidden" > Loading...</span >
                     </div >`;
    if (active == true) {
        $("#container-spinner").html(spinner);
    }
    else {
        $("#container-spinner").remove("div");
    }
}
