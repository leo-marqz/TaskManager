
export class Task {

    #taskViewModel = null;

    constructor(taskViewModel) {
        console.log(typeof taskViewModel);
        this.#taskViewModel = taskViewModel;
    }

    AddTask(e) {
        console.log(e.target);
        this.#taskViewModel.tasks.push({ title: "tarea agregada con clase Task" });
    }
}