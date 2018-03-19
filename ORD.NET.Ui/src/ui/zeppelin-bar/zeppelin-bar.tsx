import React from 'react';
import { MainGridComponent } from '../main-grid/main-grid';
import '../../css/zeppelinbar.css';
import url from 'url';
import Zeppelin from '../../js/zeppelin';

export interface ZeppelinBarProps {
    groupId: number;
    username: string;
    theme?: string;
}

export interface ZeppelinBarState {
    zeppelins: Array<Zeppelin>;
}

export default class ZeppelinSideBarComponent extends React.Component<ZeppelinBarProps, ZeppelinBarState> {

    constructor(props: ZeppelinBarProps) {
        super(props);

        this.state = {
            zeppelins: new Array<Zeppelin>()
        };
    }

    componentWillReceiveProps(nextProps: ZeppelinBarProps) {
        if (nextProps.groupId === this.props.groupId) {
            return;
        }

        // pulisco la lista degli zeppelin se non c'è nulla selezionato
        if (nextProps.groupId === 0) {
            this.setState(() => ({
                zeppelins: []
            }));
            return;
        } else {
            fetch(url.format({
                protocol: 'http',
                host: __API_URL__,
                pathname: `/api/zeppelin`
            }))
                .then(res => res.json())
                .then(
                    (result) => {
                        this.setState({
                            zeppelins: result
                        });
                    }
                );
        }
    }

    render() {
        return ([
            <div id='zeppelin-bar' key='zeppelin-bar'>
                {this.renderZeppelins(this.state.zeppelins)}
            </div>,
            <this.renderGrid key='grid' />
        ]);
    }

    renderZeppelins(props: Array<Zeppelin>) {
        if (!props) {
            return (
                <ul></ul>
            );
        }

        const listItems = props.map((g) =>
            <li key={g.id.toString()} className='zeppelin-item' >{g.nome}</li>
        );

        return (
            <ul>{listItems}</ul>
        );
    }

    private renderGrid() {
        return (
            <MainGridComponent />
        );
    }
}
