import React, { useState } from 'react';
import { Button, Form, Input, Checkbox } from 'antd';
import { EyeInvisibleOutlined, EyeTwoTone } from '@ant-design/icons';
import '../../assets/css/login.css';

export const LoginPage = () => {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [remember, setRemember] = useState<boolean>(true);
    const [error, setError] = useState<string>('');

    const handleChangeEmail = (e: any) => {
        setEmail(e.value);
    }

    const handleChangePassword = (e: any) => {
        setPassword(e.value);
    }

    const handleCheckbox = (e: any) => {
        setRemember(e.value);
    }
    return (
        <>
            <div className="content">
                <Form>
                    <Form.Item label={'Email'} rules={[
                        {
                            type: 'email',
                            message: 'The input is not valid E-mail!',
                        },
                        {
                            required: true,
                            message: 'Please input your E-mail!',
                        }
                    ]}>
                        <Input
                            value={email}
                            onChange={handleChangeEmail}
                            placeholder={'Please enter your email.'}
                        />
                    </Form.Item>
                    <Form.Item label={'Password'} rules={[{ required: true, message: 'Please input your password!' }]}>
                        <Input.Password
                            value={password}
                            onChange={handleChangePassword}
                            placeholder="Please enter your password."
                            iconRender={(visible: any) => (visible ? <EyeTwoTone /> : <EyeInvisibleOutlined />)}
                        />
                    </Form.Item>
                    <Form.Item>
                        <Form.Item name="remember" valuePropName="checked" noStyle>
                            <Checkbox
                                value={remember}
                                onChange={handleCheckbox}
                            >
                                Remember Me
                            </Checkbox>

                        </Form.Item>

                        <a className="login-form-forgot" href="">
                            Forgot password
                        </a>
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" className="login-form-button">
                            Log in
                        </Button>
                        Or <a href="">register now!</a>
                    </Form.Item>
                </Form>
            </div >
        </>
    )
}