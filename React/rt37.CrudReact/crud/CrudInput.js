import React, { Component } from 'react';

class CrudInput extends React.Component {
    constructor(props){
        super()

        // ref 생성
        this.refUserName = React.createRef()
        this.refUserPower = React.createRef()
    }
    insert = (event)=>{
        // 이벤트 핸들러 this bind

        // 유효성 검사: 생략
        if(this.refUserName.current.value.trim() === ''){
            alert('이름을 입력하세요')
            this.refUserName.current.focus()
            event.preventDefault()
            return false
        }
        if(this.refUserName.current.value.trim() === ''){
            alert('파워를 입력하세요')
            this.refUserPower.current.focus()
            event.preventDefault()
            return false
        }
        if( isNaN( Number(this.refUserPower.current.value) ) ){
            alert('파워에 숫자를 입력하세요')
            this.refUserPower.current.value=""
            this.refUserPower.current.focus()
            event.preventDefault()
            return false
        }

        const man = {
            name: this.refUserName.current.value,
            power: Number( this.refUserPower.current.value ),
        }
        
        // 부모(CrudApp)에게 값을 넘긴다.
        this.props.insert(man)
        
        // 초기화 방법1
        this.refUserName.current.value = ""
        this.refUserPower.current.value = 0        
    }
    render() {
        
        const user = this.props.user // CrudApp.state.user
        return (
            <div>
                {/* <!-- list 데이터를 table형태로 출력하고 power가 300이상인 사람은 글자색을 red로 bold스타일로 출력하세요. --> */ }
                <h1>Creat Read Update Delete</h1>
                <div>
                    <label htmlFor="">Name : </label>
                    <input 
                        type="text" 
                        name="name" 
                        ref={this.refUserName }
                        defaultValue={user.name}
                    />
                </div>
                <div>
                    <label htmlFor="">Power : </label> 
                    <input 
                        type="text" 
                        name="power" 
                        ref={this.refUserPower }
                        defaultValue={user.power}
                    />
                </div> 
                <button onClick={this.insert}>Add</button>
            </div>
        )
    }
}

export default CrudInput;