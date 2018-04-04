import React from 'react';
import { OrderGridComponent } from '../order-grid/order-grid';
import '../../css/zeppelinbar.css';
import url from 'url';
import Zeppelin from '../../js/zeppelin';
import classNames from 'classnames';

export interface ZeppelinBarProps {
    groupId: number;
    username: string;
    theme?: string;
}

export interface ZeppelinBarState {
    zeppelins: Array<Zeppelin>;
    selectedZeppelin: number;
}

export default class ZeppelinSideBarComponent extends React.Component<ZeppelinBarProps, ZeppelinBarState> {

    constructor(props: ZeppelinBarProps) {
        super(props);

        this.state = {
            zeppelins: new Array<Zeppelin>(),
            selectedZeppelin: -1
        };
    }

    componentWillReceiveProps(nextProps: ZeppelinBarProps) {
        if (nextProps.groupId === this.props.groupId) {
            return;
        }

        // pulisco la lista degli zeppelin se non c'è nulla selezionato
        if (nextProps.groupId === null) {
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
            <OrderGridComponent
                key='grid'
                groupId={this.props.groupId}
                selectedZeppelin={this.state.selectedZeppelin}
                username={this.props.username}
                theme={this.props.theme} />
        ]);
    }

    renderZeppelins(props: Array<Zeppelin>) {
        if (!props) {
            return (
                <ul></ul>
            );
        }

        const listItems = props.map((g) => {
            const isSelected = g.id === this.state.selectedZeppelin;

            const liClasses = classNames({
                'zeppelin-item': true,
                'selected': isSelected
            });

            // const selectionMarker = isSelected ? <div className='selection-marker' /> : null;

            return <li key={g.id.toString()} data-id={g.id.toString()} className={liClasses} onClick={(e) => this.selectZeppelin(e)}>
                <span className='zeppelin-item-content'>{g.nome}</span>
            </li>;
        });

        return (
            <ul className='zeppelin-container'>{listItems}</ul>
        );
    }

    selectZeppelin(e: React.MouseEvent<HTMLLIElement>) {
        const obj = e.currentTarget.attributes.getNamedItem('data-id');
        const id = obj == null ? -1 : obj.value;

        this.setState(() => ({
            selectedZeppelin: +id
        }));
    }
}
