import React from 'react';

export default class DataGridComponent extends React.Component<{}, {}> {
    constructor(props: any) {
        super(props);
    }

    render() {
        let content = [];

        for (let i = 0; i < 100; i++) {
            content.push(i.toString());
        }

        const contents = content.map((e) => {
            return (<tr><td>{e}</td></tr>);
        });

        return (
            <table className='data-grid'>
                <thead className='data-grid-header-container'>
                    <tr>
                        <th>col1</th>
                        <th>col2</th>
                        <th>col2</th>
                        <th>col2</th>
                    </tr>
                </thead>
                <tbody className='data-grid-content'>
                    {contents}
                </tbody>
            </table>
        );
    }
}
