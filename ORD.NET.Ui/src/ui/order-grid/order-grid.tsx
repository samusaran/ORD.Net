import React from 'react';
import Order from '../../js/order';
import '../../css/order-grid.css';
import url from 'url';
import classNames from 'classnames';
// import DataGridComponent from '../data-grid/data-grid';
import ReactTable from 'react-table';
import 'react-table/react-table.css';

export interface OrderGridProps {
    groupId: number;
    selectedZeppelin: number;
    username: string;
    theme?: string;
}

export interface OrderGridState {
    orders: Array<Order>;
}

export class OrderGridComponent extends React.Component<OrderGridProps, OrderGridState> {

    /*     private _columns = [
            { key: 'id', name: 'ID' }
        ]; */

    constructor(props: OrderGridProps) {
        super(props);

        this.state = {
            orders: new Array<Order>()
        };
    }

    componentWillReceiveProps(nextProps: OrderGridProps) {
        if (nextProps.groupId === this.props.groupId &&
            nextProps.selectedZeppelin === this.props.selectedZeppelin) {
            return;
        }

        if (nextProps.selectedZeppelin === -1) {
            this.setState(() => ({
                orders: []
            }));
            return;
        }

        fetch(url.format({
            protocol: 'http',
            host: __API_URL__,
            pathname: `/api/orders/group/${nextProps.groupId}/zeppelin/${nextProps.selectedZeppelin}`
        }))
            .then(res => res.json())
            .then(
                (result) => {
                    this.setState(() => ({
                        orders: result
                    }));
                }
            );
    }

    rowGetter(i: number) {
        return this.state.orders[i];
    }

    render() {
        const classes = classNames({
            'data-grid': true,
            'hidden': this.props.selectedZeppelin === -1
        });

        const columns = [{
            Header: '',
            accessor: 'profilepic'
        }, {
            Header: 'Adepto',
            accessor: 'utenteName'
        }, {
            Header: 'Piatto scelto',
            accessor: 'piatto'
        }];

        return (
            <div id='order-grid-container'>
                <ReactTable
                    data={this.state.orders}
                    columns={columns}
                    className={classes}
                    showPageSizeOptions={false}
                    showPageJump={false}
                    showPagination={false}
                    minRows={1}
                    noDataText={'Nessun ordine presente'} />
            </div>
        );
    }
}
