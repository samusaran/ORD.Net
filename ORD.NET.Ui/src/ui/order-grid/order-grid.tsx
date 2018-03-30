import React from 'react';
import Order from '../../js/order';
import '../../css/order-grid.css';
import DataGridComponent from '../data-grid/data-grid';

export interface OrderGridProps {
    groupId?: number;
    selectedZeppelin?: number;
    username: string;
    theme?: string;
}

export interface OrderGridState {
    orders: Array<Order>;
}

export class OrderGridComponent extends React.Component<OrderGridProps, OrderGridState> {

    private _columns = [
        { key: 'id', name: 'ID' }
    ];

    constructor(props: OrderGridProps) {
        super(props);

        this.state = {
            orders: new Array<Order>()
        };
    }

    rowGetter(i: number) {
        return this.state.orders[i];
    }

    render() {
        return (
            <div id='order-grid-container'>
                <DataGridComponent
                    columns={this._columns}
                    rowGetter={(index) => this.rowGetter(index)}
                    rowsCount={this.state.orders.length} />
            </div>
        );
    }
}
