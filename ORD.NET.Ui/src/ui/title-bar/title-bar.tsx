import React from 'react'
import { remote } from 'electron'

const currentWindow = remote.getCurrentWindow()

export interface TitleBarProps {
    disableMaximize: boolean;
    disableMinimize: boolean;
    title: string;
}

interface TitleBarState {
    isMaximized: boolean;
    disableMaximize: boolean;
    disableMinimize: boolean;
    title: string;
}

export class TitleBar extends React.Component<TitleBarProps, TitleBarState> {
    constructor(props: TitleBarProps) {
        super(props);
        this.state = {
            isMaximized: currentWindow.isMaximized(),
            disableMinimize: props.disableMinimize || false,
            disableMaximize: props.disableMaximize || false,
            title: props.title || ''
        }
    }

    componentDidMount() {
        currentWindow.addListener('maximize', () => this.setState({ isMaximized: true }))
        currentWindow.addListener('unmaximize', () => this.setState({ isMaximized: false }))
    }

    render() {
        return (
            <div className='titlebar draggable'>
                <div className='titlebar-title'>{this.state.title}</div>
                <div className='titlebar-controls'>
                    <button aria-label='minimize' tabIndex={-1} className='titlebar-control titlebar-minimize' disabled={this.state.disableMinimize}
                        onClick={() => currentWindow.minimize()}>
                        <svg aria-hidden='true' version='1.1' width='10' height='10'>
                            <path d='M 0,5 10,5 10,6 0,6 Z' />
                        </svg>
                    </button>
                    <button aria-label='maximize' tabIndex={-1} className='titlebar-control titlebar-resize' disabled={this.state.disableMaximize}
                        onClick={() => currentWindow.isMaximizable() ? currentWindow.isMaximized() ? currentWindow.unmaximize() : currentWindow.maximize() : null}>
                        <svg aria-hidden='true' version='1.1' width='10' height='10'>
                            <path d={this.state.isMaximized ? 'm 2,1e-5 0,2 -2,0 0,8 8,0 0,-2 2,0 0,-8 z m 1,1 6,0 0,6 -1,0 0,-5 -5,0 z m -2,2 6,0 0,6 -6,0 z' : 'M 0,0 0,10 10,10 10,0 Z M 1,1 9,1 9,9 1,9 Z'} />
                        </svg>
                    </button>
                    <button aria-label='close' tabIndex={-1} className='titlebar-control titlebar-close'
                        onClick={() => currentWindow.close()}>
                        <svg aria-hidden='true' version='1.1' width='10' height='10'>
                            <path d='M 0,0 0,0.7 4.3,5 0,9.3 0,10 0.7,10 5,5.7 9.3,10 10,10 10,9.3 5.7,5 10,0.7 10,0 9.3,0 5,4.3 0.7,0 Z' />
                        </svg>
                    </button>
                </div>
            </div>
        )
    }
}
