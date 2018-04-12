import React from 'react';
import Order from '../../js/order';
import '../../css/order-grid.css';
import url from 'url';
import classNames from 'classnames';

export interface OrderGridProps {
    groupId: number;
    selectedZeppelin: number;
    username: string;
    theme?: string;
}

export interface OrderGridState {
    orders: Array<Order>;
    selectedOrder: number;
}

export class OrderGridComponent extends React.Component<OrderGridProps, OrderGridState> {

    /*     private _columns = [
            { key: 'id', name: 'ID' }
        ]; */

    constructor(props: OrderGridProps) {
        super(props);

        this.state = {
            orders: new Array<Order>(),
            selectedOrder: -1
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

    renderOrders(orders: Array<Order>) {
        const listItems = orders.map((o) => {
            const isSelected = o.idOrdinazione === this.state.selectedOrder;

            const liClasses = classNames({
                'order-list-item': true,
                'selected': isSelected
            });

            return <li key={o.idOrdinazione.toString()} data-id={o.idOrdinazione.toString()} className={liClasses}>
                <div className='profile-pic-small'></div>
                <div className='single-order-text-container'>
                    <div className='user-name'>{o.utenteName}</div>
                    <div className='order-text'>{o.piatto}</div>
                </div>
            </li>;
        });

        return listItems;
    }

    render() {
        const controlClasses = classNames({
            'hidden': this.props.selectedZeppelin === -1
        });

        const contentClasses = classNames({
            'hidden': this.props.selectedZeppelin === -1,
            '-no-orders': this.state.orders.length === 0
        });

        return (
            <div id='order-list-container'>
                <div id='order-list-controls' className={controlClasses}>
                    <div className='order-list-control'>
                        <svg className='order-list-new' viewBox='0 0 19 19'>
                            <path d='M8 4 h3 v4 h4 v3 h-4 v4 h-3 v-4 h-4 v-3 h4 z' />
                        </svg>
                    </div>
                    <div className='order-list-control'>
                        <svg className='order-list-remove' viewBox='0 0 19 19'>
                            <path d='M4 8 h11 v3 h-11 v-3 z' />
                        </svg>
                    </div>
                </div>
                <div id='order-list-content' className={contentClasses}>
                    <ul>
                        {this.renderOrders(this.state.orders)}
                    </ul>
                </div>
            </div>
        );
    }
}
