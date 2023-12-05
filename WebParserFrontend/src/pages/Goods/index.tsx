import { request } from "@/.umi/plugin-request";
import { PageContainer, ProColumns, ProForm, ProFormCheckbox, ProFormSelect, ProFormText, ProTable } from "@ant-design/pro-components"
import { Space, message } from "antd";
import { useEffect, useState } from "react";
import { Name, Product, Shop } from "typings";

const cols: ProColumns<Product>[] = [
    {
        renderText(text, record, index, action) {
            return record.name.name;
        },
        title: 'Название',
    },
    {
        dataIndex: 'url',
        title: 'Url'
    },
    {
        dataIndex: 'lastFetchedPrice',
        title: 'Цена'
    },
    {
        renderText(text, record, index, action) {
            return record.shop.name
        },
        title: 'Магазин'
    }
]

export default () => {
    const [shopsVals, setShopsVals] = useState<{value: number, label: string}[]>([]);
    const [namesVals, setNamesVals] = useState<{value: number, label: string}[]>([]);
    const [prods, setProds] = useState<Product[]>([]);

    const [isNewName, setIsNewName] = useState<boolean>(false);

    useEffect(() => {
        request("https://localhost:7169/api/Shops/GetShops")
            .then((shops: Shop[]) => setShopsVals(shops.map(s => (
                {
                    value: s.shopId,
                    label: s.name
                }
            ))))
            .catch(() => message.error("Не могу подгрузить магазины("));

        request("https://localhost:7169/api/Products/GetNames")
            .then((names: Name[]) => setNamesVals(names.map(n => (
                {
                    value: n.productsNamesId,
                    label: n.name
                }
            ))))
            .catch(() => message.error("Не могу подгрузить названия("))

        request("https://localhost:7169/api/products/getproducts")
                .then((prods: Product[]) => setProds(prods))
                .catch(() => message.error("Не могу подгрузить товары("));
    }, []);

    const formFinishHandler = async (data: any) => {
        let newName: Name | null = null;
        if (data.newName) {
            newName = await request(`https://localhost:7169/api/products/addname?name=${data.newName}`);
        }

        request("https://localhost:7169/api/products/addproduct", {
            method: "POST",
            data: {...data, nameId: newName?.productsNamesId ?? data.nameId}
        })
            .then((porduct: Product) => setProds([...prods, porduct]))
            .catch(() => message.error("error"));
    }

    return <PageContainer>
        <ProForm onFinish={formFinishHandler}>
            <ProFormText name="url" label="Ссылка на товар"/>
            <ProFormSelect name="shopId" label="Магазин" options={shopsVals}/>
            <Space direction="vertical" style={{width: '100%'}}>
                <ProFormCheckbox fieldProps={{
                    onChange(e) {
                        const val = e.target.checked;
                        setIsNewName(val);
                    },
                    defaultChecked: isNewName
                }}
                label="Новое название?"/>
                {isNewName
                ? <ProFormText name="newName" label="Название"/>
                : <ProFormSelect name="nameId" label="Название" options={namesVals}/>}
            </Space>
        </ProForm>
        <br/>
        <ProTable
        search={false}
        columns={cols}
        dataSource={prods}/>
    </PageContainer>
}