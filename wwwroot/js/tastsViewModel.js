import "/js/knockout-min.js";


function TaskViewModelFn() {
    let self = this;
    self.tasks = ko.observableArray([]);
    self.loading = ko.observable(true);
    self.noTask = ko.pureComputed(() => {
        if (self.loading()) {
            return false;
        }
        return self.tasks().length === 0;
    });
}

function TaskItem({ id, title }) {
    let self = this;
    self.id = ko.observable(id);
    self.title = ko.observable(title);
    self.isNew = ko.pureComputed(function () {
        return self.id() === 0;
    });
}

const taskViewModel = new TaskViewModelFn();

setTimeout(() => taskViewModel.loading(false), 100);

//add
document.getElementById("add-task").addEventListener("click", (e) => {
    addTask(e);
});

//delete
document.getElementById("delete-task").addEventListener("click", (e) => {
    deleteTask(e);
});


ko.applyBindings(taskViewModel, document.getElementById('container__task'));

function handleFocusOnTaskTitle(task) {
    const title = task.title();
    if (!title) {
        taskViewModel.tasks.pop();
        return;
    }
    task.id(1);
}

function addTask(e) {
    console.log(e.target);
    taskViewModel.tasks.push(new TaskItem({ id: 0, title: '' }));
    $("[name=taskTitle]").last().focus();
}

function deleteTask(e) {
    console.log(e.target);
    taskViewModel.tasks.pop();
}