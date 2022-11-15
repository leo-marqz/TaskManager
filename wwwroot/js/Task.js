export class Task {

    constructor(id = 0, title = "", description = "") {
        this.id = id;
        this.title = title;
        this.description = description;
    }

    set Id(value) {
        this.id = value;
    }
    get Id() {
        return this.id;
    }

    set Title(value) {
        this.title = value;
    }
    get Title() {
        return this.title;
    }

    set Description(value) {
        this.description = value;
    }
    get Description() {
        return this.description;
    }
}