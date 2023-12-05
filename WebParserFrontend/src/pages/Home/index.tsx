import Guide from '@/components/Guide';
import { trim } from '@/utils/format';
import { PageContainer, ProColumns, ProTable } from '@ant-design/pro-components';
import { request, useModel } from '@umijs/max';
import styles from './index.less';
import { BestOffer } from 'typings';
import { useEffect, useState } from 'react';
import { message } from 'antd';

const HomePage: React.FC = () => {

  const proTableCols: ProColumns<BestOffer>[] = [
    {
      dataIndex: "url",
      title: "url"
    },
    {
      dataIndex: "bestPrice",
      title: "price"
    },
    {
      dataIndex: "name",
      title: "name"
    }
  ]
  
  const [bestOffers, setBestOffers] = useState<BestOffer[]>([]);

  useEffect(() => {
    request("https://localhost:7169/api/Products/GetBestOffers", {method: "GET"})
      .then((offers: BestOffer[]) => setBestOffers(offers))
      .catch(() => message.error("Offers are not available yet."))
  }, []);

  return (
    <PageContainer ghost>
      <ProTable<BestOffer> 
      columns={proTableCols}
      dataSource={bestOffers}
      search={false}
      />
    </PageContainer>
  );
};

export default HomePage;
