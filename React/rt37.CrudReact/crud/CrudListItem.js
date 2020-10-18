import React, { Component } from 'react';

class CrudListItem extends React.Component {
    state = {
        isEditMode: false
    }
    constructor(props){
        super(props)

        // ref 생성
        this.refUserName = React.createRef()
        this.refUserPower = React.createRef()
    }
    doDel=(event)=>{
        // 이벤트 핸들러는 화살표 함수로 만들면 this를 bind() 생략해도 된다
        console.log(event.target)
        
        const {id} = this.props.man //const id = this.props.man.id
        this.props.doDel(id) // CurdApp.func.doDel() 호출
    }
    doUp=(event)=>{
        // 이벤트 핸들러는 화살표 함수로 만들면 this를 bind() 생략해도 된다
        console.log(event.target)
        
        const {id} = this.props.man //const id = this.props.man.id
        this.props.doUp(id) // CurdApp.func.doUp() 호출
    }
    doDown=(event)=>{
        // 이벤트 핸들러는 화살표 함수로 만들면 this를 bind() 생략해도 된다
        console.log(event.target)
        
        const {id} = this.props.man //const id = this.props.man.id
        this.props.doDown(id) // CurdApp.func.doDown() 호출
    }
    doEdit=(event)=>{
        // 이벤트 핸들러는 화살표 함수로 만들면 this를 bind() 생략해도 된다
        console.log(event.target)
        
        this.setState( (prevState, prop)=>{
            return {
                isEditMode: !prevState.isEditMode
            }
        })
    }
    doSave = (event)=>{
        // 이벤트 핸들러는 화살표 함수로 만들면 this를 bind() 생략해도 된다
        console.log(event.target)
        
        //isEditMode 바꾸기 
        this.setState( (prevState, prop)=>{
            return {
                isEditMode: !prevState.isEditMode
            }
        })
        
        const item = this.props.man
        const man = {
            id: item.id,
            name: this.refUserName.current.value,
            power: Number(this.refUserPower.current.value)
        }
        
        this.props.doEdit(man) // CrudApp.func.doEdit() 호출 
    }
    render() {
        const item = this.props.man
        const formView = (
            <tr key={item.id} className={"strong"}>
                <td>{item.id}</td>
                <td>{item.name}</td>
                <td>{item.power}</td>
                <td>
                    <button onClick={this.doUp}>Power Up</button>
                    <button onClick={this.doDown}>Power Down</button>
                    <button onClick={this.doEdit}>Edit</button>
                    <button onClick={this.doDel}>Del</button>
                </td>
            </tr>
        )

        const formEdit = (
            <tr key={item.id} className={"strong"}>
                <td>{item.id}</td>
                <td>
                    <input 
                        type="text" 
                        name="name" 
                        ref={this.refUserName }
                        defaultValue={item.name}
                    />
                </td>
                <td>
                    <input 
                        type="text" 
                        name="power" 
                        ref={this.refUserPower }
                        defaultValue={item.power}
                    />
                </td>
                <td>
                    <button onClick={this.doUp}>Power Up</button>
                    <button onClick={this.doDown}>Power Down</button>
                    <button onClick={this.doSave}>Save</button>
                </td>
            </tr> 
        )
        
        if( this.state.isEditMode ){
            return formEdit
        }
        else{
            return formView
        }
    }
}

export default CrudListItem;