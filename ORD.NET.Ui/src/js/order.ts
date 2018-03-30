export default class Order {
    idOrdinazione: number;
    data: Date;
    gruppo: number;
    utenteName: string;
    zeppelinID?: number;
    piatto?: string;
    shottini: boolean;

    constructor(idOrdinazione: number,
        data: Date,
        gruppo: number,
        utenteName: string,
        shottini: boolean,
        zeppelinID?: number,
        piatto?: string) {
        this.idOrdinazione = idOrdinazione;
        this.data = data;
        this.gruppo = gruppo;
        this.utenteName = utenteName;
        this.zeppelinID = zeppelinID;
        this.piatto = piatto;
        this.shottini = shottini;
    }

}
