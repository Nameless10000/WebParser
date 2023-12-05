import { PageContainer, ProColumns, ProForm, ProFormText, ProTable } from "@ant-design/pro-components"
import { request } from "@umijs/max"
import { message } from "antd";
import { useEffect, useState } from "react";
import { Shop } from "typings";

const cols: ProColumns<Shop>[] = [
    {
        title: "номер",
        renderText(text, record, index, action) {
            return `${index}`;
        },
        width: 30
    },
    {
        title: "Название",
        dataIndex: "name"
    },
]

export default () => {

    const [shops, setShops] = useState<Shop[]>([]);

    useEffect(() => {
        request("https://localhost:7169/api/Shops/GetShops")
            .then((shops: Shop[]) => setShops(shops))
            .catch(() => message.error("Не могу загрузить магазины, сорян"));
    }, []);


    const addShopHandler = async (data: {name: string}) => {
        const response: Shop = await request("https://localhost:7169/api/Shops/AddShop", {
            method: "POST",
            data
        });

        if (response){
            message.success("Магазин добавлен");
            setShops([...shops, response]);
        }
        else 
            message.error("Все пошло по *?!*?!");
    }

    return <PageContainer>
        <ProForm
        onFinish={addShopHandler}
        title="Добавление магазина"
        >
            <ProFormText name="name" required rules={[{required: true, message: "Enter shop name"}]}/>
        </ProForm>
        <br />
        <ProTable
        search={false} 
        columns={cols}
        dataSource={shops}/>
    </PageContainer>
}