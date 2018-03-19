export default class Zeppelin {
    id: number;
    nome: string;
    email: string;

    constructor(id: number,
                name: string,
                email: string) {
        this.id = id;
        this.nome = name;
        this.email = email;
    }
}
